using Iycons_web2._0.Data;
using Iycons_web2._0.Model;

namespace Iycons_web2._0.Service
{
    public class ImageStorageService
    {
        private readonly Context _context;
        private readonly string _imageDirectory;

        public ImageStorageService(Context context, IConfiguration configuration)
        {
            _context = context;
            _imageDirectory = configuration.GetSection("AppSettings:ImageDirectory").Value;
        }

        public async Task<Media> SaveImage(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                // Handle invalid file
                return null;
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(_imageDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            var imageModel = new Media
            {
                Filename = fileName,
                FilePath = filePath
            };

            _context.Medias.Add(imageModel);
            await _context.SaveChangesAsync();

            return imageModel;
        }
    }

}
