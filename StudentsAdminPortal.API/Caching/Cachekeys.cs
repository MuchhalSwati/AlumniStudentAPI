namespace StudentsAdminPortal.API.Caching
{
    public static class Cachekeys
    {
        public static string Student = "student";

        public static string GetStudentCacheKey(int universityId, int departmentId)
        {
            return $"{Student}_{universityId}_{departmentId}";
         
        }
    }
}
