namespace Balls.Source.View.UI.Elements
{
    public static class ValueTranslator
    {
        private static readonly string[] Units = {
            "", "K", "M", "B"
        };

        public static string Translate(float value)
        {
            int unitIndex = 0;

            while (value / 1000 >= 1)
            {
                value /= 1000;
                unitIndex++;
            }

            return $"{value + Units[unitIndex]}";
        }
    }
}