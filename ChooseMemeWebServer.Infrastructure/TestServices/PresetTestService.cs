using ChooseMemeWebServer.Application.Interfaces;
using ChooseMemeWebServer.Core.Entities;

namespace ChooseMemeWebServer.Infrastructure.TestServices
{
    public class PresetTestService : IPresetService
    {
        public static Dictionary<string, Preset> Presets = new Dictionary<string, Preset>();

        public async Task<Preset> CreatePreset(string name)
        {
            Preset preset = new Preset();
            preset.Id = (Presets.Count + 1).ToString();
            preset.Name = name;


            var random = new Random();
            string[] sampleQuestions = {
                "What is your favorite hobby?",
                "How do you define success?",
                "What motivates you every day?",
                "What is your dream job?",
                "If you could travel anywhere, where would it be?",
                "What advice would you give your younger self?",
                "What is the best book you've ever read?",
                "What do you enjoy doing in your free time?",
                "What is one skill you'd love to master?",
                "If you won the lottery, what would you do?",
                "Who inspires you the most and why?",
                "What was your most memorable childhood moment?",
                "If you could meet any historical figure, who would it be?",
                "What is your favorite way to spend a weekend?",
                "How do you handle stress?",
                "What is your biggest accomplishment?",
                "What is your dream vacation destination?",
                "If you could have any superpower, what would it be?",
                "What is one thing you cannot live without?",
                "If you could switch lives with anyone for a day, who would it be?",
                "What is your favorite quote?",
                "What is your favorite type of music?",
                "Do you prefer books or movies?",
                "What was your favorite subject in school?",
                "If you could start a business, what would it be?",
                "What is something new you've learned recently?",
                "How do you usually start your day?",
                "What is your favorite holiday and why?",
                "If you could time travel, would you go to the past or future?",
                "What is your guilty pleasure?",
                "What is one thing you want to achieve this year?",
                "What is your favorite childhood memory?",
                "How do you stay productive?",
                "What is your go-to comfort food?",
                "If you had unlimited money, what would you do first?",
                "What is your biggest fear?",
                "If you had to describe yourself in one word, what would it be?",
                "What is the best piece of advice you've ever received?",
                "How do you like to spend your free time?",
                "What is your favorite movie of all time?",
                "If you could learn any language instantly, which one would you pick?",
                "What motivates you to keep going during tough times?",
                "Who is your favorite fictional character?",
                "What do you value most in friendships?",
                "If you had to eat one meal for the rest of your life, what would it be?",
                "What is one thing you'd change about the world?",
                "How do you define happiness?",
                "What is the best gift you've ever received?",
                "If you could have dinner with three people, dead or alive, who would they be?",
                "What do you think is the most important quality in a leader?",
                "What do you do when you need inspiration?",
                "If you could master one instrument, which would it be?"
            };

            string[] fileExtensions = { ".jpg", ".png", ".mp4", ".mp3", ".pdf" };

            var questions = new List<Question>();
            var media = new List<Media>();

            for (int i = 0; i < 50; i++)
            {
                questions.Add(new Question()
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = sampleQuestions[random.Next(sampleQuestions.Length)],
                    Preset = preset
                });

                media.Add(new Media()
                {
                    Id = Guid.NewGuid().ToString(),
                    FileName = $"file_{i + 1}{fileExtensions[random.Next(fileExtensions.Length)]}",
                    Preset = preset
                });
            }

            preset.Questions = questions;
            preset.Media = media;
            Presets.Add(preset.Id, preset);

            await Task.Delay(0);

            return preset;
        }

        public void DeletePreset(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Preset> GetPreset(string id)
        {
            await Task.Delay(0);
            Presets.TryGetValue(id, out var preset);

            if (preset == null)
            {
                throw new KeyNotFoundException();
            }

            return preset;
        }
    }
}
