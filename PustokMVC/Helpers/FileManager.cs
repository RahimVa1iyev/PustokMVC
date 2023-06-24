namespace PustokMVC.Helpers
{
    public class FileManager
    {
        public static string Save(IFormFile file, string rootPath,string folder)
        {

            var newFileName = Guid.NewGuid().ToString() + (file.FileName.Length <= 64 ? file.FileName : file.FileName.Substring(file.FileName.Length - 64));
            var path = rootPath  + folder + newFileName;

            using (var stream = new FileStream(path, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return newFileName;
        }
    }
}
