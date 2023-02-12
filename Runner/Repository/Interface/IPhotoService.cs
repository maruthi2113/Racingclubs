using CloudinaryDotNet.Actions;
using Microsoft.Extensions.FileProviders;

namespace Runner.Repository.Interface
{
    public interface IPhotoService
    {

        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string publicId);

    }
}
