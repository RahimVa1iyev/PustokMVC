namespace PustokMVC.Helpers
{
    public static class FileManager
    {
        public static string Save(IFormFile file , string rootpath,string folder)
        {

            var newFormFile = Guid.NewGuid().ToString() + (file.FileName.Length <= 64 ? file.FileName : file.FileName.Substring(file.FileName.Length - 64));

            var path = Path.Combine(rootpath, folder, newFormFile);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return newFormFile;

        }
    }
}
