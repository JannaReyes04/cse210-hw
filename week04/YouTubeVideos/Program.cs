using System;
using System.Collections.Generic;

namespace YouTubeVideos
{
    // Class representing a Comment
    public class Comment
    {
        public string CommenterName { get; }
        public string CommentText { get; }

        public Comment(string commenterName, string commentText)
        {
            CommenterName = commenterName;
            CommentText = commentText;
        }

        public override string ToString()
        {
            return $"{CommenterName}: {CommentText}";
        }
    }

    // Class representing a Video
    public class Video
    {
        public string Title { get; }
        public string Author { get; }
        public int LengthInSeconds { get; }
        private List<Comment> Comments { get; }

        public Video(string title, string author, int lengthInSeconds)
        {
            Title = title;
            Author = author;
            LengthInSeconds = lengthInSeconds;
            Comments = new List<Comment>();
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public int GetNumberOfComments()
        {
            return Comments.Count;
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Length: {LengthInSeconds / 60}m {LengthInSeconds % 60}s");
            Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");
            Console.WriteLine("Comments:");
            foreach (var comment in Comments)
            {
                Console.WriteLine($"  {comment}");
            }
            Console.WriteLine(new string('-', 40));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create video objects
            var video1 = new Video("Introduction to C#", "Jane Doe", 360);
            var video2 = new Video("Advanced C# Techniques", "John Smith", 540);
            var video3 = new Video("C# and .NET Framework Basics", "Alice Johnson", 450);

            // Add comments to video1
            video1.AddComment(new Comment("User123", "Great introduction!"));
            video1.AddComment(new Comment("DevGuy", "Clear and concise, thanks!"));
            video1.AddComment(new Comment("CodeQueen", "Could you make one on LINQ next?"));

            // Add comments to video2
            video2.AddComment(new Comment("TechGuru", "Fantastic explanation of async programming."));
            video2.AddComment(new Comment("CodeLover", "Loved the examples!"));
            video2.AddComment(new Comment("BugFinder", "Please slow down next time."));

            // Add comments to video3
            video3.AddComment(new Comment("DotNetFan", "Very helpful, thank you!"));
            video3.AddComment(new Comment("NewbieCoder", "I finally understand the .NET framework."));
            video3.AddComment(new Comment("SeniorDev", "Good refresher for experienced devs."));

            // Create a list of videos
            var videos = new List<Video> { video1, video2, video3 };

            // Display details of each video
            foreach (var video in videos)
            {
                video.DisplayDetails();
            }
        }
    }
}
