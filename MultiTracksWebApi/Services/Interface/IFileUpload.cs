using MultiTracksWebApi.Response;

namespace MultiTracksWebApi.Services.Interface
{
    public interface IFileUpload
    {
        UploadFileResponse UploadAvatar(IFormFile file);
        CloudinaryDotNet.Actions.DeletionResult DeleteAvatar(string publicId);
    }
}
