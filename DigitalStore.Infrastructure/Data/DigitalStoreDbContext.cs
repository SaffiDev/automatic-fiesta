using System;
using System.Collections.Generic;
using DigitalStore.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Object = DigitalStore.Infrastructure.Data.Models.Object;

namespace DigitalStore.Infrastructure.Data;

public partial class DigitalStoreDbContext : DbContext
{
    public DigitalStoreDbContext()
    {
    }

    public DigitalStoreDbContext(DbContextOptions<DigitalStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AuditLogEntry> AuditLogEntries { get; set; }

    public virtual DbSet<Bucket> Buckets { get; set; }

    public virtual DbSet<BucketsAnalytic> BucketsAnalytics { get; set; }

    public virtual DbSet<BucketsVector> BucketsVectors { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DigitalContent> DigitalContents { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<DownloadHistory> DownloadHistories { get; set; }

    public virtual DbSet<FlowState> FlowStates { get; set; }

    public virtual DbSet<Identity> Identities { get; set; }

    public virtual DbSet<Instance> Instances { get; set; }

    public virtual DbSet<MfaAmrClaim> MfaAmrClaims { get; set; }

    public virtual DbSet<MfaChallenge> MfaChallenges { get; set; }

    public virtual DbSet<MfaFactor> MfaFactors { get; set; }

    public virtual DbSet<Migration> Migrations { get; set; }

    public virtual DbSet<OauthAuthorization> OauthAuthorizations { get; set; }

    public virtual DbSet<OauthClient> OauthClients { get; set; }

    public virtual DbSet<OauthClientState> OauthClientStates { get; set; }

    public virtual DbSet<OauthConsent> OauthConsents { get; set; }

    public virtual DbSet<Object> Objects { get; set; }

    public virtual DbSet<OneTimeToken> OneTimeTokens { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<PaymentTransaction> PaymentTransactions { get; set; }

    public virtual DbSet<Prefix> Prefixes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<S3MultipartUpload> S3MultipartUploads { get; set; }

    public virtual DbSet<S3MultipartUploadsPart> S3MultipartUploadsParts { get; set; }

    public virtual DbSet<SamlProvider> SamlProviders { get; set; }

    public virtual DbSet<SamlRelayState> SamlRelayStates { get; set; }

    public virtual DbSet<SchemaMigration> SchemaMigrations { get; set; }

    public virtual DbSet<SchemaMigration1> SchemaMigrations1 { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<SsoDomain> SsoDomains { get; set; }

    public virtual DbSet<SsoProvider> SsoProviders { get; set; }

    public virtual DbSet<Subscription> Subscriptions { get; set; }

    public virtual DbSet<User1> Users { get; set; }

    public virtual DbSet<User1> Users1 { get; set; }

    public virtual DbSet<UserPaymentMethod> UserPaymentMethods { get; set; }

    public virtual DbSet<UserWishlist> UserWishlists { get; set; }

    public virtual DbSet<VectorIndex> VectorIndexes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DatabaseSeeder.Seed(modelBuilder);
        base.OnModelCreating(modelBuilder);
        modelBuilder
            .HasPostgresEnum("auth", "aal_level", new[] { "aal1", "aal2", "aal3" })
            .HasPostgresEnum("auth", "code_challenge_method", new[] { "s256", "plain" })
            .HasPostgresEnum("auth", "factor_status", new[] { "unverified", "verified" })
            .HasPostgresEnum("auth", "factor_type", new[] { "totp", "webauthn", "phone" })
            .HasPostgresEnum("auth", "oauth_authorization_status", new[] { "pending", "approved", "denied", "expired" })
            .HasPostgresEnum("auth", "oauth_client_type", new[] { "public", "confidential" })
            .HasPostgresEnum("auth", "oauth_registration_type", new[] { "dynamic", "manual" })
            .HasPostgresEnum("auth", "oauth_response_type", new[] { "code" })
            .HasPostgresEnum("auth", "one_time_token_type", new[] { "confirmation_token", "reauthentication_token", "recovery_token", "email_change_token_new", "email_change_token_current", "phone_change_token" })
            .HasPostgresEnum("realtime", "action", new[] { "INSERT", "UPDATE", "DELETE", "TRUNCATE", "ERROR" })
            .HasPostgresEnum("realtime", "equality_op", new[] { "eq", "neq", "lt", "lte", "gt", "gte", "in" })
            .HasPostgresEnum("storage", "buckettype", new[] { "STANDARD", "ANALYTICS", "VECTOR" })
            .HasPostgresExtension("extensions", "pg_stat_statements")
            .HasPostgresExtension("extensions", "pgcrypto")
            .HasPostgresExtension("extensions", "uuid-ossp")
            .HasPostgresExtension("graphql", "pg_graphql")
            .HasPostgresExtension("vault", "supabase_vault");

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("admins_pkey");

            entity.ToTable("admins", tb => tb.HasComment("Администраторы системы"));

            entity.HasIndex(e => e.Email, "admins_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "admins_username_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата создания")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(100)
                .HasComment("Отображаемое имя")
                .HasColumnName("display_name");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasComment("Email админа")
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasComment("Хеш пароля")
                .HasColumnName("password_hash");
            entity.Property(e => e.ProfileImage)
                .HasMaxLength(255)
                .HasComment("Аватар")
                .HasColumnName("profile_image");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasDefaultValueSql("'admin'::character varying")
                .HasComment("Роль: admin, super_admin")
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата обновления")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasComment("Логин админа")
                .HasColumnName("username");
        });

        modelBuilder.Entity<AuditLogEntry>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("audit_log_entries_pkey");

            entity.ToTable("audit_log_entries", "auth", tb => tb.HasComment("Auth: Audit trail for user actions."));

            entity.HasIndex(e => e.InstanceId, "audit_logs_instance_id_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.InstanceId).HasColumnName("instance_id");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(64)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("ip_address");
            entity.Property(e => e.Payload)
                .HasColumnType("json")
                .HasColumnName("payload");
        });

        modelBuilder.Entity<Bucket>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("buckets_pkey");

            entity.ToTable("buckets", "storage");

            entity.HasIndex(e => e.Name, "bname").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AllowedMimeTypes).HasColumnName("allowed_mime_types");
            entity.Property(e => e.AvifAutodetection)
                .HasDefaultValue(false)
                .HasColumnName("avif_autodetection");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.FileSizeLimit).HasColumnName("file_size_limit");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Owner)
                .HasComment("Field is deprecated, use owner_id instead")
                .HasColumnName("owner");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.Public)
                .HasDefaultValue(false)
                .HasColumnName("public");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<BucketsAnalytic>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("buckets_analytics_pkey");

            entity.ToTable("buckets_analytics", "storage");

            entity.HasIndex(e => e.Name, "buckets_analytics_unique_name_idx")
                .IsUnique()
                .HasFilter("(deleted_at IS NULL)");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Format)
                .HasDefaultValueSql("'ICEBERG'::text")
                .HasColumnName("format");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<BucketsVector>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("buckets_vectors_pkey");

            entity.ToTable("buckets_vectors", "storage");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cart_pkey");

            entity.ToTable("cart", tb => tb.HasComment("Корзина покупателя"));

            entity.HasIndex(e => new { e.UserId, e.ProductId }, "cart_user_id_product_id_idx").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата добавления в корзину")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("added_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasComment("Количество товара")
                .HasColumnName("quantity");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.Carts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cart_product_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("cart_user_id_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("categories_pkey");

            entity.ToTable("categories", tb => tb.HasComment("Категории товаров: музыка, книги, ПО, игры"));

            entity.HasIndex(e => e.Name, "categories_name_key").IsUnique();

            entity.HasIndex(e => e.ParentId, "categories_parent_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasComment("Описание категории")
                .HasColumnName("description");
            entity.Property(e => e.Icon)
                .HasMaxLength(255)
                .HasComment("URL иконки категории")
                .HasColumnName("icon");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasComment("Активна ли категория")
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasComment("Название категории")
                .HasColumnName("name");
            entity.Property(e => e.ParentId)
                .HasComment("Родительская категория (для вложенности)")
                .HasColumnName("parent_id");
            entity.Property(e => e.SortOrder)
                .HasDefaultValue(0)
                .HasComment("Порядок сортировки")
                .HasColumnName("sort_order");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("categories_parent_id_fkey");
        });

        modelBuilder.Entity<DigitalContent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("digital_content_pkey");

            entity.ToTable("digital_content", tb => tb.HasComment("Цифровой контент и лицензии"));

            entity.HasIndex(e => e.ProductId, "digital_content_product_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DownloadLimit)
                .HasDefaultValue(5)
                .HasComment("Лимит скачиваний")
                .HasColumnName("download_limit");
            entity.Property(e => e.DownloadUrl)
                .HasMaxLength(500)
                .HasComment("Защищенная ссылка для скачивания")
                .HasColumnName("download_url");
            entity.Property(e => e.FileHash)
                .HasMaxLength(64)
                .HasComment("SHA256 хеш файла")
                .HasColumnName("file_hash");
            entity.Property(e => e.FilePath)
                .HasMaxLength(500)
                .HasComment("Путь к файлу на сервере")
                .HasColumnName("file_path");
            entity.Property(e => e.LicenseKeyTemplate)
                .HasMaxLength(255)
                .HasComment("Шаблон для генерации лицензионных ключей")
                .HasColumnName("license_key_template");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Product).WithOne(p => p.DigitalContent)
                .HasForeignKey<DigitalContent>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("digital_content_product_id_fkey");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("discounts_pkey");

            entity.ToTable("discounts", tb => tb.HasComment("Скидки и промокоды"));

            entity.HasIndex(e => e.Code, "discounts_code_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .HasComment("Промокод")
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата создания")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.CurrentUses)
                .HasDefaultValue(0)
                .HasComment("Текущее количество использований")
                .HasColumnName("current_uses");
            entity.Property(e => e.Description)
                .HasComment("Описание акции")
                .HasColumnName("description");
            entity.Property(e => e.DiscountType)
                .HasMaxLength(20)
                .HasComment("Тип скидки: percentage (процент), fixed (фиксированная)")
                .HasColumnName("discount_type");
            entity.Property(e => e.DiscountValue)
                .HasPrecision(10, 2)
                .HasComment("Размер скидки")
                .HasColumnName("discount_value");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasComment("Активна ли скидка")
                .HasColumnName("is_active");
            entity.Property(e => e.MaxUses)
                .HasComment("Максимальное количество использований")
                .HasColumnName("max_uses");
            entity.Property(e => e.MinPurchaseAmount)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasComment("Минимальная сумма покупки")
                .HasColumnName("min_purchase_amount");
            entity.Property(e => e.ValidFrom)
                .HasComment("Действует с")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("valid_from");
            entity.Property(e => e.ValidUntil)
                .HasComment("Действует до")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("valid_until");

            entity.HasMany(d => d.Products).WithMany(p => p.Discounts)
                .UsingEntity<Dictionary<string, object>>(
                    "DiscountProduct",
                    r => r.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("discount_products_product_id_fkey"),
                    l => l.HasOne<Discount>().WithMany()
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("discount_products_discount_id_fkey"),
                    j =>
                    {
                        j.HasKey("DiscountId", "ProductId").HasName("discount_products_pkey");
                        j.ToTable("discount_products", tb => tb.HasComment("Связь скидок с конкретными товарами"));
                        j.IndexerProperty<int>("DiscountId").HasColumnName("discount_id");
                        j.IndexerProperty<int>("ProductId").HasColumnName("product_id");
                    });
        });

        modelBuilder.Entity<DownloadHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("download_history_pkey");

            entity.ToTable("download_history", tb => tb.HasComment("История скачивания файлов"));

            entity.HasIndex(e => e.OrderItemId, "download_history_order_item_id_idx");

            entity.HasIndex(e => e.UserId, "download_history_user_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DownloadedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата и время скачивания")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("downloaded_at");
            entity.Property(e => e.IpAddress)
                .HasComment("IP адрес")
                .HasColumnName("ip_address");
            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.UserAgent)
                .HasComment("Браузер и ОС")
                .HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.DownloadHistories)
                .HasForeignKey(d => d.OrderItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("download_history_order_item_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.DownloadHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("download_history_user_id_fkey");
        });

        modelBuilder.Entity<FlowState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("flow_state_pkey");

            entity.ToTable("flow_state", "auth", tb => tb.HasComment("stores metadata for pkce logins"));

            entity.HasIndex(e => e.CreatedAt, "flow_state_created_at_idx").IsDescending();

            entity.HasIndex(e => e.AuthCode, "idx_auth_code");

            entity.HasIndex(e => new { e.UserId, e.AuthenticationMethod }, "idx_user_id_auth_method");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AuthCode).HasColumnName("auth_code");
            entity.Property(e => e.AuthCodeIssuedAt).HasColumnName("auth_code_issued_at");
            entity.Property(e => e.AuthenticationMethod).HasColumnName("authentication_method");
            entity.Property(e => e.CodeChallenge).HasColumnName("code_challenge");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.ProviderAccessToken).HasColumnName("provider_access_token");
            entity.Property(e => e.ProviderRefreshToken).HasColumnName("provider_refresh_token");
            entity.Property(e => e.ProviderType).HasColumnName("provider_type");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Identity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("identities_pkey");

            entity.ToTable("identities", "auth", tb => tb.HasComment("Auth: Stores identities associated to a user."));

            entity.HasIndex(e => e.Email, "identities_email_idx").HasOperators(new[] { "text_pattern_ops" });

            entity.HasIndex(e => new { e.ProviderId, e.Provider }, "identities_provider_id_provider_unique").IsUnique();

            entity.HasIndex(e => e.UserId, "identities_user_id_idx");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasComputedColumnSql("lower((identity_data ->> 'email'::text))", true)
                .HasComment("Auth: Email is a generated column that references the optional email property in the identity_data")
                .HasColumnName("email");
            entity.Property(e => e.IdentityData)
                .HasColumnType("jsonb")
                .HasColumnName("identity_data");
            entity.Property(e => e.LastSignInAt).HasColumnName("last_sign_in_at");
            entity.Property(e => e.Provider).HasColumnName("provider");
            entity.Property(e => e.ProviderId).HasColumnName("provider_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Identities)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("identities_user_id_fkey");
        });

        modelBuilder.Entity<Instance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("instances_pkey");

            entity.ToTable("instances", "auth", tb => tb.HasComment("Auth: Manages users across multiple sites."));

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.RawBaseConfig).HasColumnName("raw_base_config");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<MfaAmrClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("amr_id_pk");

            entity.ToTable("mfa_amr_claims", "auth", tb => tb.HasComment("auth: stores authenticator method reference claims for multi factor authentication"));

            entity.HasIndex(e => new { e.SessionId, e.AuthenticationMethod }, "mfa_amr_claims_session_id_authentication_method_pkey").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AuthenticationMethod).HasColumnName("authentication_method");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(d => d.Session).WithMany(p => p.MfaAmrClaims)
                .HasForeignKey(d => d.SessionId)
                .HasConstraintName("mfa_amr_claims_session_id_fkey");
        });

        modelBuilder.Entity<MfaChallenge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mfa_challenges_pkey");

            entity.ToTable("mfa_challenges", "auth", tb => tb.HasComment("auth: stores metadata about challenge requests made"));

            entity.HasIndex(e => e.CreatedAt, "mfa_challenge_created_at_idx").IsDescending();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.FactorId).HasColumnName("factor_id");
            entity.Property(e => e.IpAddress).HasColumnName("ip_address");
            entity.Property(e => e.OtpCode).HasColumnName("otp_code");
            entity.Property(e => e.VerifiedAt).HasColumnName("verified_at");
            entity.Property(e => e.WebAuthnSessionData)
                .HasColumnType("jsonb")
                .HasColumnName("web_authn_session_data");

            entity.HasOne(d => d.Factor).WithMany(p => p.MfaChallenges)
                .HasForeignKey(d => d.FactorId)
                .HasConstraintName("mfa_challenges_auth_factor_id_fkey");
        });

        modelBuilder.Entity<MfaFactor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mfa_factors_pkey");

            entity.ToTable("mfa_factors", "auth", tb => tb.HasComment("auth: stores metadata about factors"));

            entity.HasIndex(e => new { e.UserId, e.CreatedAt }, "factor_id_created_at_idx");

            entity.HasIndex(e => e.LastChallengedAt, "mfa_factors_last_challenged_at_key").IsUnique();

            entity.HasIndex(e => new { e.FriendlyName, e.UserId }, "mfa_factors_user_friendly_name_unique")
                .IsUnique()
                .HasFilter("(TRIM(BOTH FROM friendly_name) <> ''::text)");

            entity.HasIndex(e => e.UserId, "mfa_factors_user_id_idx");

            entity.HasIndex(e => new { e.UserId, e.Phone }, "unique_phone_factor_per_user").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.FriendlyName).HasColumnName("friendly_name");
            entity.Property(e => e.LastChallengedAt).HasColumnName("last_challenged_at");
            entity.Property(e => e.LastWebauthnChallengeData)
                .HasComment("Stores the latest WebAuthn challenge data including attestation/assertion for customer verification")
                .HasColumnType("jsonb")
                .HasColumnName("last_webauthn_challenge_data");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.Secret).HasColumnName("secret");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WebAuthnAaguid).HasColumnName("web_authn_aaguid");
            entity.Property(e => e.WebAuthnCredential)
                .HasColumnType("jsonb")
                .HasColumnName("web_authn_credential");

            entity.HasOne(d => d.User).WithMany(p => p.MfaFactors)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("mfa_factors_user_id_fkey");
        });

        modelBuilder.Entity<Migration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("migrations_pkey");

            entity.ToTable("migrations", "storage");

            entity.HasIndex(e => e.Name, "migrations_name_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ExecutedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("executed_at");
            entity.Property(e => e.Hash)
                .HasMaxLength(40)
                .HasColumnName("hash");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<OauthAuthorization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauth_authorizations_pkey");

            entity.ToTable("oauth_authorizations", "auth");

            entity.HasIndex(e => e.ExpiresAt, "oauth_auth_pending_exp_idx").HasFilter("(status = 'pending'::auth.oauth_authorization_status)");

            entity.HasIndex(e => e.AuthorizationCode, "oauth_authorizations_authorization_code_key").IsUnique();

            entity.HasIndex(e => e.AuthorizationId, "oauth_authorizations_authorization_id_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ApprovedAt).HasColumnName("approved_at");
            entity.Property(e => e.AuthorizationCode).HasColumnName("authorization_code");
            entity.Property(e => e.AuthorizationId).HasColumnName("authorization_id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CodeChallenge).HasColumnName("code_challenge");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt)
                .HasDefaultValueSql("(now() + '00:03:00'::interval)")
                .HasColumnName("expires_at");
            entity.Property(e => e.Nonce).HasColumnName("nonce");
            entity.Property(e => e.RedirectUri).HasColumnName("redirect_uri");
            entity.Property(e => e.Resource).HasColumnName("resource");
            entity.Property(e => e.Scope).HasColumnName("scope");
            entity.Property(e => e.State).HasColumnName("state");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Client).WithMany(p => p.OauthAuthorizations)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("oauth_authorizations_client_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.OauthAuthorizations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("oauth_authorizations_user_id_fkey");
        });

        modelBuilder.Entity<OauthClient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauth_clients_pkey");

            entity.ToTable("oauth_clients", "auth");

            entity.HasIndex(e => e.DeletedAt, "oauth_clients_deleted_at_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClientName).HasColumnName("client_name");
            entity.Property(e => e.ClientSecretHash).HasColumnName("client_secret_hash");
            entity.Property(e => e.ClientUri).HasColumnName("client_uri");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.GrantTypes).HasColumnName("grant_types");
            entity.Property(e => e.LogoUri).HasColumnName("logo_uri");
            entity.Property(e => e.RedirectUris).HasColumnName("redirect_uris");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<OauthClientState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauth_client_states_pkey");

            entity.ToTable("oauth_client_states", "auth", tb => tb.HasComment("Stores OAuth states for third-party provider authentication flows where Supabase acts as the OAuth client."));

            entity.HasIndex(e => e.CreatedAt, "idx_oauth_client_states_created_at");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CodeVerifier).HasColumnName("code_verifier");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.ProviderType).HasColumnName("provider_type");
        });

        modelBuilder.Entity<OauthConsent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("oauth_consents_pkey");

            entity.ToTable("oauth_consents", "auth");

            entity.HasIndex(e => e.ClientId, "oauth_consents_active_client_idx").HasFilter("(revoked_at IS NULL)");

            entity.HasIndex(e => new { e.UserId, e.ClientId }, "oauth_consents_active_user_client_idx").HasFilter("(revoked_at IS NULL)");

            entity.HasIndex(e => new { e.UserId, e.ClientId }, "oauth_consents_user_client_unique").IsUnique();

            entity.HasIndex(e => new { e.UserId, e.GrantedAt }, "oauth_consents_user_order_idx").IsDescending(false, true);

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.GrantedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("granted_at");
            entity.Property(e => e.RevokedAt).HasColumnName("revoked_at");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Client).WithMany(p => p.OauthConsents)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("oauth_consents_client_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.OauthConsents)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("oauth_consents_user_id_fkey");
        });

        modelBuilder.Entity<Object>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("objects_pkey");

            entity.ToTable("objects", "storage");

            entity.HasIndex(e => new { e.BucketId, e.Name }, "bucketid_objname").IsUnique();

            entity.HasIndex(e => new { e.Name, e.BucketId, e.Level }, "idx_name_bucket_level_unique")
                .IsUnique()
                .UseCollation(new[] { "C", null, null });

            entity.HasIndex(e => new { e.BucketId, e.Name }, "idx_objects_bucket_id_name").UseCollation(new[] { null, "C" });

            entity.HasIndex(e => e.Name, "name_prefix_search").HasOperators(new[] { "text_pattern_ops" });

            entity.HasIndex(e => new { e.BucketId, e.Level, e.Name }, "objects_bucket_id_level_idx")
                .IsUnique()
                .UseCollation(new[] { null, null, "C" });

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.BucketId).HasColumnName("bucket_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.LastAccessedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("last_accessed_at");
            entity.Property(e => e.Level).HasColumnName("level");
            entity.Property(e => e.Metadata)
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Owner)
                .HasComment("Field is deprecated, use owner_id instead")
                .HasColumnName("owner");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.PathTokens)
                .HasComputedColumnSql("string_to_array(name, '/'::text)", true)
                .HasColumnName("path_tokens");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserMetadata)
                .HasColumnType("jsonb")
                .HasColumnName("user_metadata");
            entity.Property(e => e.Version).HasColumnName("version");

            entity.HasOne(d => d.Bucket).WithMany(p => p.Objects)
                .HasForeignKey(d => d.BucketId)
                .HasConstraintName("objects_bucketId_fkey");
        });

        modelBuilder.Entity<OneTimeToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("one_time_tokens_pkey");

            entity.ToTable("one_time_tokens", "auth");

            entity.HasIndex(e => e.RelatesTo, "one_time_tokens_relates_to_hash_idx").HasMethod("hash");

            entity.HasIndex(e => e.TokenHash, "one_time_tokens_token_hash_hash_idx").HasMethod("hash");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.RelatesTo).HasColumnName("relates_to");
            entity.Property(e => e.TokenHash).HasColumnName("token_hash");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.OneTimeTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("one_time_tokens_user_id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("orders_pkey");

            entity.ToTable("orders", tb => tb.HasComment("Заказы покупателей"));

            entity.HasIndex(e => e.OrderNumber, "orders_order_number_idx");

            entity.HasIndex(e => e.OrderNumber, "orders_order_number_key").IsUnique();

            entity.HasIndex(e => e.Status, "orders_status_idx");

            entity.HasIndex(e => e.UserId, "orders_user_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CompletedAt)
                .HasComment("Дата завершения")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("completed_at");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата создания заказа")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.DiscountAmount)
                .HasPrecision(10, 2)
                .HasDefaultValueSql("0")
                .HasComment("Размер скидки")
                .HasColumnName("discount_amount");
            entity.Property(e => e.DiscountCode)
                .HasMaxLength(50)
                .HasComment("Использованный промокод")
                .HasColumnName("discount_code");
            entity.Property(e => e.FinalAmount)
                .HasPrecision(10, 2)
                .HasComment("Итоговая сумма к оплате")
                .HasColumnName("final_amount");
            entity.Property(e => e.OrderNumber)
                .HasComment("Уникальный номер заказа")
                .HasColumnName("order_number");
            entity.Property(e => e.PaidAt)
                .HasComment("Дата оплаты")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("paid_at");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pending'::character varying")
                .HasComment("Статус: pending, paid, completed, cancelled, refunded")
                .HasColumnName("status");
            entity.Property(e => e.TotalAmount)
                .HasPrecision(10, 2)
                .HasComment("Сумма до скидки")
                .HasColumnName("total_amount");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentMethodId)
                .HasConstraintName("orders_payment_method_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("orders_user_id_fkey");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_items_pkey");

            entity.ToTable("order_items", tb => tb.HasComment("Позиции в заказе (что именно купили)"));

            entity.HasIndex(e => e.OrderId, "order_items_order_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DownloadExpiresAt)
                .HasComment("Срок действия ссылки для скачивания")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("download_expires_at");
            entity.Property(e => e.DownloadToken)
                .HasComment("Токен для безопасного скачивания")
                .HasColumnName("download_token");
            entity.Property(e => e.DownloadsRemaining)
                .HasDefaultValue(5)
                .HasComment("Осталось скачиваний")
                .HasColumnName("downloads_remaining");
            entity.Property(e => e.LicenseKey)
                .HasMaxLength(255)
                .HasComment("Лицензионный ключ (для ПО)")
                .HasColumnName("license_key");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasComment("Название товара на момент покупки")
                .HasColumnName("product_name");
            entity.Property(e => e.Quantity)
                .HasComment("Количество")
                .HasColumnName("quantity");
            entity.Property(e => e.TotalPrice)
                .HasPrecision(10, 2)
                .HasComment("Общая стоимость")
                .HasColumnName("total_price");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasComment("Цена за единицу")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_items_order_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_items_product_id_fkey");
        });

        modelBuilder.Entity<PaymentTransaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_transactions_pkey");

            entity.ToTable("payment_transactions", tb => tb.HasComment("История платежных транзакций"));

            entity.HasIndex(e => e.OrderId, "payment_transactions_order_id_idx");

            entity.HasIndex(e => e.TransactionId, "payment_transactions_transaction_id_idx");

            entity.HasIndex(e => e.TransactionId, "payment_transactions_transaction_id_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasComment("Сумма платежа")
                .HasColumnName("amount");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата транзакции")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasDefaultValueSql("'RUB'::character varying")
                .HasComment("Валюта")
                .HasColumnName("currency");
            entity.Property(e => e.ErrorMessage)
                .HasComment("Сообщение об ошибке")
                .HasColumnName("error_message");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.PaymentSystem)
                .HasMaxLength(50)
                .HasComment("Платежная система: yookassa, stripe, paypal")
                .HasColumnName("payment_system");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasComment("Статус: pending, success, failed")
                .HasColumnName("status");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .HasComment("ID транзакции в платежной системе")
                .HasColumnName("transaction_id");

            entity.HasOne(d => d.Order).WithMany(p => p.PaymentTransactions)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_transactions_order_id_fkey");
        });

        modelBuilder.Entity<Prefix>(entity =>
        {
            entity.HasKey(e => new { e.BucketId, e.Level, e.Name }).HasName("prefixes_pkey");

            entity.ToTable("prefixes", "storage");

            entity.Property(e => e.BucketId).HasColumnName("bucket_id");
            entity.Property(e => e.Level)
                .HasComputedColumnSql("storage.get_level(name)", true)
                .HasColumnName("level");
            entity.Property(e => e.Name)
                .UseCollation("C")
                .HasColumnName("name");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Bucket).WithMany(p => p.Prefixes)
                .HasForeignKey(d => d.BucketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prefixes_bucketId_fkey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("products_pkey");

            entity.ToTable("products", tb => tb.HasComment("Цифровые товары магазина"));

            entity.HasIndex(e => e.CategoryId, "products_category_id_idx");

            entity.HasIndex(e => e.IsActive, "products_is_active_idx");

            entity.HasIndex(e => e.ProductType, "products_product_type_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Author)
                .HasMaxLength(100)
                .HasComment("Автор или исполнитель")
                .HasColumnName("author");
            entity.Property(e => e.AvgRating)
                .HasPrecision(3, 2)
                .HasComment("Средний рейтинг")
                .HasColumnName("avg_rating");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата добавления")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasComment("Описание товара")
                .HasColumnName("description");
            entity.Property(e => e.FileSizeMb)
                .HasPrecision(10, 2)
                .HasComment("Размер файла в МБ")
                .HasColumnName("file_size_mb");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasComment("Доступен ли товар")
                .HasColumnName("is_active");
            entity.Property(e => e.Language)
                .HasMaxLength(10)
                .HasDefaultValueSql("'ru'::character varying")
                .HasComment("Язык товара")
                .HasColumnName("language");
            entity.Property(e => e.Metadata)
                .HasComment("Дополнительные характеристики (битрейт, формат, и т.д.)")
                .HasColumnType("jsonb")
                .HasColumnName("metadata");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasComment("Название товара")
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasComment("Цена в рублях")
                .HasColumnName("price");
            entity.Property(e => e.ProductType)
                .HasMaxLength(20)
                .HasComment("Тип: music, book, software, game")
                .HasColumnName("product_type");
            entity.Property(e => e.Publisher)
                .HasMaxLength(100)
                .HasComment("Издатель")
                .HasColumnName("publisher");
            entity.Property(e => e.ReleaseDate)
                .HasComment("Дата выхода")
                .HasColumnName("release_date");
            entity.Property(e => e.ReviewCount)
                .HasDefaultValue(0)
                .HasComment("Количество отзывов")
                .HasColumnName("review_count");
            entity.Property(e => e.SalesCount)
                .HasDefaultValue(0)
                .HasComment("Количество продаж")
                .HasColumnName("sales_count");
            entity.Property(e => e.StockQuantity)
                .HasDefaultValue(0)
                .HasComment("Количество лицензий")
                .HasColumnName("stock_quantity");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата обновления")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Version)
                .HasMaxLength(50)
                .HasComment("Версия (для ПО)")
                .HasColumnName("version");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("products_category_id_fkey");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_images_pkey");

            entity.ToTable("product_images", tb => tb.HasComment("Изображения товаров"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasComment("URL изображения")
                .HasColumnName("image_url");
            entity.Property(e => e.IsPrimary)
                .HasDefaultValue(false)
                .HasComment("Главное изображение товара")
                .HasColumnName("is_primary");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.SortOrder)
                .HasDefaultValue(0)
                .HasComment("Порядок отображения")
                .HasColumnName("sort_order");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_images_product_id_fkey");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("refresh_tokens_pkey");

            entity.ToTable("refresh_tokens", "auth", tb => tb.HasComment("Auth: Store of tokens used to refresh JWT tokens once they expire."));

            entity.HasIndex(e => e.InstanceId, "refresh_tokens_instance_id_idx");

            entity.HasIndex(e => new { e.InstanceId, e.UserId }, "refresh_tokens_instance_id_user_id_idx");

            entity.HasIndex(e => e.Parent, "refresh_tokens_parent_idx");

            entity.HasIndex(e => new { e.SessionId, e.Revoked }, "refresh_tokens_session_id_revoked_idx");

            entity.HasIndex(e => e.Token, "refresh_tokens_token_unique").IsUnique();

            entity.HasIndex(e => e.UpdatedAt, "refresh_tokens_updated_at_idx").IsDescending();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.InstanceId).HasColumnName("instance_id");
            entity.Property(e => e.Parent)
                .HasMaxLength(255)
                .HasColumnName("parent");
            entity.Property(e => e.Revoked).HasColumnName("revoked");
            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .HasColumnName("token");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UserId)
                .HasMaxLength(255)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Session).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.SessionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("refresh_tokens_session_id_fkey");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("reviews_pkey");

            entity.ToTable("reviews", tb => tb.HasComment("Отзывы и рейтинги товаров"));

            entity.HasIndex(e => e.IsApproved, "reviews_is_approved_idx");

            entity.HasIndex(e => e.ProductId, "reviews_product_id_idx");

            entity.HasIndex(e => new { e.ProductId, e.UserId }, "reviews_product_id_user_id_idx").IsUnique();

            entity.HasIndex(e => e.UserId, "reviews_user_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasComment("Текст отзыва")
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата создания")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.HelpfulCount)
                .HasDefaultValue(0)
                .HasComment("Количество отметок \"полезно\"")
                .HasColumnName("helpful_count");
            entity.Property(e => e.IsApproved)
                .HasDefaultValue(false)
                .HasComment("Одобрен модератором")
                .HasColumnName("is_approved");
            entity.Property(e => e.IsVerifiedPurchase)
                .HasDefaultValue(false)
                .HasComment("Подтвержденная покупка")
                .HasColumnName("is_verified_purchase");
            entity.Property(e => e.OrderId)
                .HasComment("Ссылка на заказ для подтверждения покупки")
                .HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Rating)
                .HasComment("Оценка от 1 до 5 звезд")
                .HasColumnName("rating");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasComment("Заголовок отзыва")
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата редактирования")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Order).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("reviews_order_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reviews_product_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reviews_user_id_fkey");
        });

        modelBuilder.Entity<S3MultipartUpload>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("s3_multipart_uploads_pkey");

            entity.ToTable("s3_multipart_uploads", "storage");

            entity.HasIndex(e => new { e.BucketId, e.Key, e.CreatedAt }, "idx_multipart_uploads_list").UseCollation(new[] { null, "C", null });

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BucketId).HasColumnName("bucket_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.InProgressSize)
                .HasDefaultValue(0L)
                .HasColumnName("in_progress_size");
            entity.Property(e => e.Key)
                .UseCollation("C")
                .HasColumnName("key");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.UploadSignature).HasColumnName("upload_signature");
            entity.Property(e => e.UserMetadata)
                .HasColumnType("jsonb")
                .HasColumnName("user_metadata");
            entity.Property(e => e.Version).HasColumnName("version");

            entity.HasOne(d => d.Bucket).WithMany(p => p.S3MultipartUploads)
                .HasForeignKey(d => d.BucketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("s3_multipart_uploads_bucket_id_fkey");
        });

        modelBuilder.Entity<S3MultipartUploadsPart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("s3_multipart_uploads_parts_pkey");

            entity.ToTable("s3_multipart_uploads_parts", "storage");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.BucketId).HasColumnName("bucket_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.Etag).HasColumnName("etag");
            entity.Property(e => e.Key)
                .UseCollation("C")
                .HasColumnName("key");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.PartNumber).HasColumnName("part_number");
            entity.Property(e => e.Size)
                .HasDefaultValue(0L)
                .HasColumnName("size");
            entity.Property(e => e.UploadId).HasColumnName("upload_id");
            entity.Property(e => e.Version).HasColumnName("version");

            entity.HasOne(d => d.Bucket).WithMany(p => p.S3MultipartUploadsParts)
                .HasForeignKey(d => d.BucketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("s3_multipart_uploads_parts_bucket_id_fkey");

            entity.HasOne(d => d.Upload).WithMany(p => p.S3MultipartUploadsParts)
                .HasForeignKey(d => d.UploadId)
                .HasConstraintName("s3_multipart_uploads_parts_upload_id_fkey");
        });

        modelBuilder.Entity<SamlProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("saml_providers_pkey");

            entity.ToTable("saml_providers", "auth", tb => tb.HasComment("Auth: Manages SAML Identity Provider connections."));

            entity.HasIndex(e => e.EntityId, "saml_providers_entity_id_key").IsUnique();

            entity.HasIndex(e => e.SsoProviderId, "saml_providers_sso_provider_id_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AttributeMapping)
                .HasColumnType("jsonb")
                .HasColumnName("attribute_mapping");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.EntityId).HasColumnName("entity_id");
            entity.Property(e => e.MetadataUrl).HasColumnName("metadata_url");
            entity.Property(e => e.MetadataXml).HasColumnName("metadata_xml");
            entity.Property(e => e.NameIdFormat).HasColumnName("name_id_format");
            entity.Property(e => e.SsoProviderId).HasColumnName("sso_provider_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(d => d.SsoProvider).WithMany(p => p.SamlProviders)
                .HasForeignKey(d => d.SsoProviderId)
                .HasConstraintName("saml_providers_sso_provider_id_fkey");
        });

        modelBuilder.Entity<SamlRelayState>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("saml_relay_states_pkey");

            entity.ToTable("saml_relay_states", "auth", tb => tb.HasComment("Auth: Contains SAML Relay State information for each Service Provider initiated login."));

            entity.HasIndex(e => e.CreatedAt, "saml_relay_states_created_at_idx").IsDescending();

            entity.HasIndex(e => e.ForEmail, "saml_relay_states_for_email_idx");

            entity.HasIndex(e => e.SsoProviderId, "saml_relay_states_sso_provider_id_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.FlowStateId).HasColumnName("flow_state_id");
            entity.Property(e => e.ForEmail).HasColumnName("for_email");
            entity.Property(e => e.RedirectTo).HasColumnName("redirect_to");
            entity.Property(e => e.RequestId).HasColumnName("request_id");
            entity.Property(e => e.SsoProviderId).HasColumnName("sso_provider_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(d => d.FlowState).WithMany(p => p.SamlRelayStates)
                .HasForeignKey(d => d.FlowStateId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("saml_relay_states_flow_state_id_fkey");

            entity.HasOne(d => d.SsoProvider).WithMany(p => p.SamlRelayStates)
                .HasForeignKey(d => d.SsoProviderId)
                .HasConstraintName("saml_relay_states_sso_provider_id_fkey");
        });

        modelBuilder.Entity<SchemaMigration>(entity =>
        {
            entity.HasKey(e => e.Version).HasName("schema_migrations_pkey");

            entity.ToTable("schema_migrations", "auth", tb => tb.HasComment("Auth: Manages updates to the auth system."));

            entity.Property(e => e.Version)
                .HasMaxLength(255)
                .HasColumnName("version");
        });

        modelBuilder.Entity<SchemaMigration1>(entity =>
        {
            entity.HasKey(e => e.Version).HasName("schema_migrations_pkey");

            entity.ToTable("schema_migrations", "realtime");

            entity.Property(e => e.Version)
                .ValueGeneratedNever()
                .HasColumnName("version");
            entity.Property(e => e.InsertedAt)
                .HasColumnType("timestamp(0) without time zone")
                .HasColumnName("inserted_at");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sessions_pkey");

            entity.ToTable("sessions", "auth", tb => tb.HasComment("Auth: Stores session data associated to a user."));

            entity.HasIndex(e => e.NotAfter, "sessions_not_after_idx").IsDescending();

            entity.HasIndex(e => e.OauthClientId, "sessions_oauth_client_id_idx");

            entity.HasIndex(e => e.UserId, "sessions_user_id_idx");

            entity.HasIndex(e => new { e.UserId, e.CreatedAt }, "user_id_created_at_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.FactorId).HasColumnName("factor_id");
            entity.Property(e => e.Ip).HasColumnName("ip");
            entity.Property(e => e.NotAfter)
                .HasComment("Auth: Not after is a nullable column that contains a timestamp after which the session should be regarded as expired.")
                .HasColumnName("not_after");
            entity.Property(e => e.OauthClientId).HasColumnName("oauth_client_id");
            entity.Property(e => e.RefreshTokenCounter)
                .HasComment("Holds the ID (counter) of the last issued refresh token.")
                .HasColumnName("refresh_token_counter");
            entity.Property(e => e.RefreshTokenHmacKey)
                .HasComment("Holds a HMAC-SHA256 key used to sign refresh tokens for this session.")
                .HasColumnName("refresh_token_hmac_key");
            entity.Property(e => e.RefreshedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("refreshed_at");
            entity.Property(e => e.Scopes).HasColumnName("scopes");
            entity.Property(e => e.Tag).HasColumnName("tag");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.UserAgent).HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.OauthClient).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.OauthClientId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("sessions_oauth_client_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("sessions_user_id_fkey");
        });

        modelBuilder.Entity<SsoDomain>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sso_domains_pkey");

            entity.ToTable("sso_domains", "auth", tb => tb.HasComment("Auth: Manages SSO email address domain mapping to an SSO Identity Provider."));

            entity.HasIndex(e => e.SsoProviderId, "sso_domains_sso_provider_id_idx");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Domain).HasColumnName("domain");
            entity.Property(e => e.SsoProviderId).HasColumnName("sso_provider_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

            entity.HasOne(d => d.SsoProvider).WithMany(p => p.SsoDomains)
                .HasForeignKey(d => d.SsoProviderId)
                .HasConstraintName("sso_domains_sso_provider_id_fkey");
        });

        modelBuilder.Entity<SsoProvider>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sso_providers_pkey");

            entity.ToTable("sso_providers", "auth", tb => tb.HasComment("Auth: Manages SSO identity provider information; see saml_providers for SAML."));

            entity.HasIndex(e => e.ResourceId, "sso_providers_resource_id_pattern_idx").HasOperators(new[] { "text_pattern_ops" });

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Disabled).HasColumnName("disabled");
            entity.Property(e => e.ResourceId)
                .HasComment("Auth: Uniquely identifies a SSO provider according to a user-chosen resource ID (case insensitive), useful in infrastructure as code.")
                .HasColumnName("resource_id");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<Subscription>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_subscription");

            entity.ToTable("subscription", "realtime");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Claims)
                .HasColumnType("jsonb")
                .HasColumnName("claims");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("timezone('utc'::text, now())")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscription_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users", "auth", tb => tb.HasComment("Auth: Stores user login data within a secure schema."));

            entity.HasIndex(e => e.ConfirmationToken, "confirmation_token_idx")
                .IsUnique()
                .HasFilter("((confirmation_token)::text !~ '^[0-9 ]*$'::text)");

            entity.HasIndex(e => e.EmailChangeTokenCurrent, "email_change_token_current_idx")
                .IsUnique()
                .HasFilter("((email_change_token_current)::text !~ '^[0-9 ]*$'::text)");

            entity.HasIndex(e => e.EmailChangeTokenNew, "email_change_token_new_idx")
                .IsUnique()
                .HasFilter("((email_change_token_new)::text !~ '^[0-9 ]*$'::text)");

            entity.HasIndex(e => e.ReauthenticationToken, "reauthentication_token_idx")
                .IsUnique()
                .HasFilter("((reauthentication_token)::text !~ '^[0-9 ]*$'::text)");

            entity.HasIndex(e => e.RecoveryToken, "recovery_token_idx")
                .IsUnique()
                .HasFilter("((recovery_token)::text !~ '^[0-9 ]*$'::text)");

            entity.HasIndex(e => e.Email, "users_email_partial_key")
                .IsUnique()
                .HasFilter("(is_sso_user = false)");

            entity.HasIndex(e => e.InstanceId, "users_instance_id_idx");

            entity.HasIndex(e => e.IsAnonymous, "users_is_anonymous_idx");

            entity.HasIndex(e => e.Phone, "users_phone_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Aud)
                .HasMaxLength(255)
                .HasColumnName("aud");
            entity.Property(e => e.BannedUntil).HasColumnName("banned_until");
            entity.Property(e => e.ConfirmationSentAt).HasColumnName("confirmation_sent_at");
            entity.Property(e => e.ConfirmationToken)
                .HasMaxLength(255)
                .HasColumnName("confirmation_token");
            entity.Property(e => e.ConfirmedAt)
                .HasComputedColumnSql("LEAST(email_confirmed_at, phone_confirmed_at)", true)
                .HasColumnName("confirmed_at");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.EmailChange)
                .HasMaxLength(255)
                .HasColumnName("email_change");
            entity.Property(e => e.EmailChangeConfirmStatus)
                .HasDefaultValue((short)0)
                .HasColumnName("email_change_confirm_status");
            entity.Property(e => e.EmailChangeSentAt).HasColumnName("email_change_sent_at");
            entity.Property(e => e.EmailChangeTokenCurrent)
                .HasMaxLength(255)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("email_change_token_current");
            entity.Property(e => e.EmailChangeTokenNew)
                .HasMaxLength(255)
                .HasColumnName("email_change_token_new");
            entity.Property(e => e.EmailConfirmedAt).HasColumnName("email_confirmed_at");
            entity.Property(e => e.EncryptedPassword)
                .HasMaxLength(255)
                .HasColumnName("encrypted_password");
            entity.Property(e => e.InstanceId).HasColumnName("instance_id");
            entity.Property(e => e.InvitedAt).HasColumnName("invited_at");
            entity.Property(e => e.IsAnonymous)
                .HasDefaultValue(false)
                .HasColumnName("is_anonymous");
            entity.Property(e => e.IsSsoUser)
                .HasDefaultValue(false)
                .HasComment("Auth: Set this column to true when the account comes from SSO. These accounts can have duplicate emails.")
                .HasColumnName("is_sso_user");
            entity.Property(e => e.IsSuperAdmin).HasColumnName("is_super_admin");
            entity.Property(e => e.LastSignInAt).HasColumnName("last_sign_in_at");
            entity.Property(e => e.Phone)
                .HasDefaultValueSql("NULL::character varying")
                .HasColumnName("phone");
            entity.Property(e => e.PhoneChange)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("phone_change");
            entity.Property(e => e.PhoneChangeSentAt).HasColumnName("phone_change_sent_at");
            entity.Property(e => e.PhoneChangeToken)
                .HasMaxLength(255)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("phone_change_token");
            entity.Property(e => e.PhoneConfirmedAt).HasColumnName("phone_confirmed_at");
            entity.Property(e => e.RawAppMetaData)
                .HasColumnType("jsonb")
                .HasColumnName("raw_app_meta_data");
            entity.Property(e => e.RawUserMetaData)
                .HasColumnType("jsonb")
                .HasColumnName("raw_user_meta_data");
            entity.Property(e => e.ReauthenticationSentAt).HasColumnName("reauthentication_sent_at");
            entity.Property(e => e.ReauthenticationToken)
                .HasMaxLength(255)
                .HasDefaultValueSql("''::character varying")
                .HasColumnName("reauthentication_token");
            entity.Property(e => e.RecoverySentAt).HasColumnName("recovery_sent_at");
            entity.Property(e => e.RecoveryToken)
                .HasMaxLength(255)
                .HasColumnName("recovery_token");
            entity.Property(e => e.Role)
                .HasMaxLength(255)
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
        });

        modelBuilder.Entity<User1>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users", tb => tb.HasComment("Зарегистрированные пользователи магазина"));

            entity.HasIndex(e => e.Email, "users_email_idx");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "users_username_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата регистрации")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasComment("Email для входа")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasComment("Имя")
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasComment("Фамилия")
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasComment("Хеш пароля")
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasComment("Телефон")
                .HasColumnName("phone");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата обновления")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasComment("Уникальный логин")
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserPaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_payment_methods_pkey");

            entity.ToTable("user_payment_methods", tb => tb.HasComment("Сохраненные способы оплаты пользователей"));

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CardType)
                .HasMaxLength(20)
                .HasComment("Тип карты: visa, mastercard, mir")
                .HasColumnName("card_type");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата добавления")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiryDate)
                .HasMaxLength(7)
                .HasComment("Срок действия MM/YY")
                .HasColumnName("expiry_date");
            entity.Property(e => e.IsDefault)
                .HasDefaultValue(false)
                .HasComment("Карта по умолчанию")
                .HasColumnName("is_default");
            entity.Property(e => e.LastFour)
                .HasMaxLength(4)
                .HasComment("Последние 4 цифры карты")
                .HasColumnName("last_four");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserPaymentMethods)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_payment_methods_user_id_fkey");
        });

        modelBuilder.Entity<UserWishlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_wishlist_pkey");

            entity.ToTable("user_wishlist", tb => tb.HasComment("Избранные товары пользователей"));

            entity.HasIndex(e => new { e.UserId, e.ProductId }, "user_wishlist_user_id_product_id_idx").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("now()")
                .HasComment("Дата добавления в избранное")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("added_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Product).WithMany(p => p.UserWishlists)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_wishlist_product_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserWishlists)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_wishlist_user_id_fkey");
        });

        modelBuilder.Entity<VectorIndex>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vector_indexes_pkey");

            entity.ToTable("vector_indexes", "storage");

            entity.HasIndex(e => new { e.Name, e.BucketId }, "vector_indexes_name_bucket_id_idx")
                .IsUnique()
                .UseCollation(new[] { "C", null });

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.BucketId).HasColumnName("bucket_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("created_at");
            entity.Property(e => e.DataType).HasColumnName("data_type");
            entity.Property(e => e.Dimension).HasColumnName("dimension");
            entity.Property(e => e.DistanceMetric).HasColumnName("distance_metric");
            entity.Property(e => e.MetadataConfiguration)
                .HasColumnType("jsonb")
                .HasColumnName("metadata_configuration");
            entity.Property(e => e.Name)
                .UseCollation("C")
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("now()")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Bucket).WithMany(p => p.VectorIndices)
                .HasForeignKey(d => d.BucketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vector_indexes_bucket_id_fkey");
        });
        modelBuilder.HasSequence<int>("seq_schema_version", "graphql").IsCyclic();

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
