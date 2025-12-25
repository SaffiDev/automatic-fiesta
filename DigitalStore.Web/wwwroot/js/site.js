window.downloadFile = (url, filename) => {
    const a = document.createElement('a');
    a.href = url;
    a.download = filename || 'file.zip'; 
    document.body.appendChild(a);
    a.click();
    document.body.removeChild(a);
};