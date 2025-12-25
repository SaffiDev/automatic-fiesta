using Supabase;
using Supabase.Gotrue;
using Blazored.LocalStorage;
using DigitalStore.Application.DTOs;
using Client = Supabase.Client;

namespace DigitalStore.Web.Services;

public class AuthService
{
    private readonly Client _supabase;
    private readonly ILocalStorageService _localStorage;

    public event Action? OnAuthStateChanged;

    public User? CurrentUser { get; private set; }
    public Session? CurrentSession { get; private set; }
    public string? CurrentUserRole { get; private set; } = "user";

    public bool IsAuthenticated => CurrentUser != null;
    public bool IsGuest { get; private set; }

    private const string GUEST_KEY = "is_guest";

    public AuthService(Client supabase, ILocalStorageService localStorage)
    {
        _supabase = supabase;
        _localStorage = localStorage;
    }

    public async Task InitializeAsync()
    {
        _supabase.Auth.AddStateChangedListener(OnAuthStateChangedInternal);

        _supabase.Auth.LoadSession();

        CurrentSession = _supabase.Auth.CurrentSession;
        CurrentUser = CurrentSession?.User;

        if (CurrentUser != null)
        {
            IsGuest = false;
            await _localStorage.RemoveItemAsync(GUEST_KEY);
            await LoadUserRoleAsync();
        }
        else
        {
            IsGuest = await _localStorage.GetItemAsync<bool>(GUEST_KEY);
        }

        OnAuthStateChanged?.Invoke();
    }

    // ← ИСПРАВЛЕНО: убрали async void → обычный void + fire-and-forget
    private void OnAuthStateChangedInternal(
        Supabase.Gotrue.Interfaces.IGotrueClient<User, Session> sender,
        Supabase.Gotrue.Constants.AuthState state)
    {
        CurrentSession = _supabase.Auth.CurrentSession;
        CurrentUser = CurrentSession?.User;

        if (CurrentUser != null)
        {
            IsGuest = false;
            _ = _localStorage.RemoveItemAsync(GUEST_KEY); // fire-and-forget
            _ = LoadUserRoleAsync(); // fire-and-forget
        }
        else
        {
            CurrentUserRole = "user";
            IsGuest = true;
        }

        OnAuthStateChanged?.Invoke();
    }

    private async Task LoadUserRoleAsync()
    {
        if (CurrentUser == null)
        {
            CurrentUserRole = "user";
            return;
        }

        try
        {
            var response = await _supabase
                .From<ProfileDto>()
                .Where(p => p.AuthUserId == CurrentUser.Id)
                .Single();

            CurrentUserRole = response?.Role ?? "user";
        }
        catch
        {
            CurrentUserRole = "user";
        }
    }

    public async Task<bool> SignInAsync(string email, string password)
    {
        try
        {
            var session = await _supabase.Auth.SignIn(email, password);
            CurrentSession = session;
            CurrentUser = session?.User;

            if (CurrentUser != null)
            {
                IsGuest = false;
                await _localStorage.RemoveItemAsync(GUEST_KEY);
                await LoadUserRoleAsync();
            }

            OnAuthStateChanged?.Invoke();
            return CurrentUser != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("SIGN IN ERROR: " + ex.Message);
            return false;
        }
    }

    public async Task<bool> SignUpAsync(string email, string password)
    {
        try
        {
            var session = await _supabase.Auth.SignUp(email, password);
            CurrentSession = session;
            CurrentUser = session?.User;

            if (CurrentUser != null)
            {
                IsGuest = false;
                await _localStorage.RemoveItemAsync(GUEST_KEY);
                await LoadUserRoleAsync();
            }

            OnAuthStateChanged?.Invoke();
            return CurrentUser != null;
        }
        catch (Exception ex)
        {
            Console.WriteLine("SIGN UP ERROR: " + ex.Message);
            return false;
        }
    }

    public async Task SignOutAsync()
    {
        await _supabase.Auth.SignOut();
        CurrentSession = null;
        CurrentUser = null;
        CurrentUserRole = "user";
        IsGuest = false;
        await _localStorage.RemoveItemAsync(GUEST_KEY);
        OnAuthStateChanged?.Invoke();
    }

    public async Task SignInAsGuestAsync()
    {
        CurrentUser = null;
        CurrentSession = null;
        CurrentUserRole = "user";
        IsGuest = true;
        await _localStorage.SetItemAsync(GUEST_KEY, true);
        OnAuthStateChanged?.Invoke();
    }

    public string? GetUserId() => CurrentUser?.Id;
}