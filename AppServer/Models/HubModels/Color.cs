namespace AppServer.Models.HubModels
{
    public struct Color
    {
        public Color( int red, int green, int blue )
        {
            this.Red = (byte)red;
            this.Green = (byte)green;
            this.Blue = (byte)blue;
        }

        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
    }
}