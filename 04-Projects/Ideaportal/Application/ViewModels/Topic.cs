using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdeasCount { get; set; }
        public string[] Gradient { get; set; }
    }

    public class TopicViewModel
    {
        public IList<Topic> Topics { get; set; }
    }

}
