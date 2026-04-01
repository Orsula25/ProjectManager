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

        // ROLE 
        public bool IsManager
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                if (context is null)
                    return false;

                return bool.TryParse(context.Session.GetString("IsManager"), out bool result) && result;
            }
            set
            {
                var context = _httpContextAccessor.HttpContext;
                if (context is null)
                    return;

                context.Session.SetString("IsManager", value.ToString());
            }
        }

        // Employee Name
        public string? FirstName
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                if (context is null)
                    return null;

                return context.Session.GetString("FirstName");
            }
            set
            {
                var context = _httpContextAccessor.HttpContext;
                if (context is null)
                    return;

                if (value is null)
                    context.Session.Remove("FirstName");
                else
                    context.Session.SetString("FirstName", value);
            }
        }

        public string? LastName
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                if (context is null)
                    return null;

                return context.Session.GetString("LastName");
            }
            set
            {
                var context = _httpContextAccessor.HttpContext;
                if (context is null)
                    return;

                if (value is null)
                    context.Session.Remove("LastName");
                else
                    context.Session.SetString("LastName", value);
            }
        }

        // logout

        public void Clear()
        {
            _httpContextAccessor.HttpContext?.Session.Clear();
        }
    }
}
