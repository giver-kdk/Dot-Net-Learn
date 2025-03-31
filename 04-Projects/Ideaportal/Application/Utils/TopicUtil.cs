using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils
{
    public static class TopicUtil
    {
        public static IList<Topic> GetTopics()=> [
        new() { Id = 1, Title = "R&D", Description = "Exploring new technologies and innovative solutions to drive the future of our products.", IdeasCount = 5, Gradient = ["#2F9395", "#45B649"] },
        new() { Id = 2, Title = "SAM", Description = "Researching and integrating AI capabilities into our applications to enhance functionality and intelligence.", IdeasCount = 10, Gradient = ["#355C7D", "#6C5B7B"] },
        new() { Id = 3, Title = "Agent-Based Architecture", Description = "Investigating scalable, service-oriented architectures where independent agents perform specialized tasks for greater flexibility.", IdeasCount = 15, Gradient = ["#FF6B6B", "#FFE66D"] },
        ];


        public static IList<Idea> GetIdeas(int topicId)
        {
            return topicId switch
            {
                1 => new[]
                {
            new Idea { Id = 1,Name = "Ideaportal", Description = "Developing Ideaportal to support collaborative R&D by generating innovative ideas for future products and solutions." },
            new Idea { Id = 2, Name = "Blockchain for Research", Description = "Researching the use of blockchain technology to secure data and create transparent systems in product development." },
            new Idea { Id = 3, Name = "AI Research", Description = "Investigating how AI can accelerate product development by automating complex research processes." },
            new Idea { Id = 4, Name = "Edge Computing", Description = "Exploring how edge computing can help process data closer to the source and improve latency in our systems." },
            new Idea { Id = 5, Name = "Autonomous Systems", Description = "Researching how autonomous systems can enhance product functionality and improve user experiences." }
        },
                2 => new[]
                {
            new Idea { Id = 6, Name = "SAM AI Model for Data Analysis", Description = "Integrating SAM AI into data analytics pipelines to enhance predictive capabilities." },
            new Idea { Id = 7, Name = "Natural Language Processing (NLP) with SAM", Description = "Leveraging SAM AI to improve natural language understanding for better user interactions." },
            new Idea { Id = 8, Name = "Personalized Recommendations with SAM", Description = "Using SAM AI to develop personalized content recommendations based on user behavior." },
            new Idea { Id = 9, Name = "AI-Powered Automation", Description = "Integrating SAM AI into business processes to automate repetitive tasks and improve efficiency." },
            new Idea { Id = 10,Name = "AI-Enhanced Customer Support", Description = "Leveraging SAM AI to enhance customer support by providing intelligent chatbots and predictive solutions." }
        },
                3 => new[]
                {
            new Idea { Id = 11, Name = "Microservices with Agent-Based Architecture", Description = "Researching how agent-based architecture can be used to build scalable and resilient microservices." },
            new Idea { Id = 12, Name = "Decentralized Agents for Load Balancing", Description = "Investigating how decentralized agents can autonomously balance workloads across distributed systems." },
            new Idea { Id = 13, Name = "Agent-Based Security Systems", Description = "Exploring how autonomous agents can be used for proactive security monitoring in a distributed environment." },
            new Idea { Id = 14, Name = "Event-Driven Agent Communication", Description = "Researching how agents in agent-based architectures can communicate through event-driven mechanisms for improved efficiency." },
            new Idea { Id = 15, Name = "Agent-Based Workflow Automation", Description = "Developing workflows where agents autonomously handle tasks and collaborate to complete larger goals in distributed systems." }
        },
                _ => Array.Empty<Idea>() // Default case for an invalid topicId
            };
        }

    }
}
