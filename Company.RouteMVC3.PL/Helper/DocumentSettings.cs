namespace Company.RouteMVC3.PL.Helper
{
    public static class DocumentSettings
    {
        //1.Upload

        public static string Upload(IFormFile file , string folderName)
        {
            //1. Get Location of folder

            //string folderPath = $"C:\\Users\\Omar\\source\\repos\\Company.RouteMVC3 Solution\\Company.RouteMVC3.PL\\wwwroot\\files\\{folderName}";

            string folderPath = Path.Combine(Directory.GetCurrentDirectory() , $"wwwroot\\files\\{folderName}");

            //2. Get File Name , Make sure it's Unique

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3. Get File Path : FolderPath + FileName

            string filePath = Path.Combine(folderPath , fileName);

            //4. File Stream : Data Per Second

            using var FileStream = new FileStream(filePath,FileMode.Create);

            file.CopyTo(FileStream);

            return fileName;
        }

        //2.Delete

        public static void Delete(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\files\\{folderName}" , fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
