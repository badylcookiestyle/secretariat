using System;
using System.Collections.Generic;
using System.Text;
using secretary;
    public static class Serializers
    {
        public static string serializeLessons(List<Lesson> lessons)
        {
            string text = "";
            lessons.ForEach(delegate (Lesson lesson) {
                text += "name: " + lesson.name + " time: " + lesson.lessonTime + "\n";
            });
            return text;
        }
        public static string serializeGroups(List<String> groups)
        {
            string text = "";
            groups.ForEach(delegate (string group) {
                text += group + "\n";
            });
            return text;
        }
    }
