using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager.BLL.Entities
{
    public class Post
    {

        public Guid PostId { get; private set; }

        private string _subject;

        public string Subject
        {
            get { return _subject; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(Subject));
                _subject = value;
            }
        }

        private string _content;

        public string Content
        {
            get { return _content; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException(nameof(Content));
                _content = value;
            }
        }

        public DateTime SendDate { get; private set; }

        public Guid EmployeeId { get; private set; }

        public Guid ProjectId { get; private set; }


        // constructeur dal->bll

        public Post(Guid postId, string subject, string content, DateTime sendDate, Guid employeeId, Guid projectId)
        {
            PostId = postId;
            Subject = subject;
            Content = content;
            SendDate = sendDate;
            EmployeeId = employeeId;
            ProjectId = projectId;
        }

        // constructeur creation d'un nouveau post

        public Post(string subject, string content, Guid employeeId, Guid projectId)
        {
       
            Subject = subject;
            Content = content;
            EmployeeId = employeeId;
            ProjectId = projectId;
            SendDate = DateTime.Now;
        }

        // modifier le contenu

        public void UpdateContent(string newContent)
        {
            Content = newContent;
            SendDate = DateTime.Now;
        }



    }
}
