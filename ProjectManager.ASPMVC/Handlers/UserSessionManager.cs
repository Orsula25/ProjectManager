namespace ProjectManager.ASPMVC.Handlers
{
    public class UserSessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserSessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // récuperer ou stocker l'utilisateur conecte 

        public Guid? EmployeeId

        {
            get
            {
                var context = _httpContextAccessor.HttpContext;

                if (context is null)
                    return null;

                string? value = context.Session.GetString("EmployeeId");

                if (Guid.TryParse(value, out Guid result))
                    return result;

                return null;

            }
            set
            {
                var context = _httpContextAccessor.HttpContext;

                if (context is null)
                    return;

                if (value is null)
                    context.Session.Remove("EmployeeId");
                else
                    context.Session.SetString("EmployeeId", value.ToString());
            }
        }

        // logout

        public void Clear()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
        }
    }
}
