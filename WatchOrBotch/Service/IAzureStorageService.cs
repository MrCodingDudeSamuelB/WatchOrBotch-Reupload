namespace WatchOrBotch.Service
{
    public interface IAzureStorageService
    {
        public Task<string> UploadFileAsync(IFormFile file, string filename);
    }
}
