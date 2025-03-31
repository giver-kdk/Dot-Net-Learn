using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class IdeaViewModel
    {
        public Topic Topic { get; set; }
        public IList<Idea> Ideas { get; set; }
        public bool ShowAddBtn { get; set; }
        public bool ShowTrash { get; set; }
    }
    public class Idea
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPublic { get; set; }
        public bool IsDropped { get; set; }
    }
}
