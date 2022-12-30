using System;
using System.Collections.Generic;
using System.Linq;

namespace Alemanha
{
    internal class Program
    {
        private static Dictionary<string, string> inputA;
        private static Dictionary<string, string> inputB;
        private static List<Dictionary<string, string>> inputs;

        static void Main(string[] args)
        {
            FillDictionaries();
            
            var result = get_same_time_of_online_presence(inputs);

            foreach (var date in result)
            {
                Console.WriteLine("{0} - {1}", date.Key.ToString(@"hh\:mm"), date.Value.ToString(@"hh\:mm"));
            }
        
            Console.ReadKey();
        }

        private static void FillDictionaries()
        {
            inputs = new List<Dictionary<string, string>>();
            inputA = new Dictionary<string, string>();
            inputB = new Dictionary<string, string>();

            inputA.Add("3:00", "4:00");
            inputA.Add("8:30", "10:00");
            inputA.Add("10:15", "12:00");
            inputA.Add("17:00", "21:00");
            inputA.Add("22:00", "22:30");

            inputB.Add("5:00", "11:15");
            inputB.Add("14:25", "20:05");
            inputB.Add("20:10", "21:00");
            inputB.Add("22:31", "22:50");

            inputs.Add(inputA);
            inputs.Add(inputB);
        }

        private static Dictionary<TimeSpan, TimeSpan> get_same_time_of_online_presence(IEnumerable<Dictionary<string, string>> users)
        {
            var userFullData1 = users.FirstOrDefault();
            var userFullData2 = users.LastOrDefault();

            TimeSpan user1_from_time;
            TimeSpan user1_to_time;

            TimeSpan user2_from_time;
            TimeSpan user2_to_time;

            var result = new Dictionary<TimeSpan, TimeSpan>();

            foreach ( var user1 in userFullData1) 
            {
                TimeSpan.TryParse(user1.Key, out user1_from_time);
                TimeSpan.TryParse(user1.Value, out user1_to_time);

                foreach (var user2 in userFullData2)
                {
                    TimeSpan.TryParse(user2.Key, out user2_from_time);
                    TimeSpan.TryParse(user2.Value, out user2_to_time);

                    if (user1_from_time.TotalMilliseconds <= user2_to_time.TotalMilliseconds && user1_to_time.TotalMilliseconds >= user2_from_time.TotalMilliseconds)
                    {
                        result.Add(
                                    (user1_from_time >= user2_from_time ? user1_from_time : user2_from_time),
                                    (user1_to_time >= user2_to_time ? user2_to_time : user1_to_time)
                                    );
                    }
                }
            }
            return result;
        }
    }
}
