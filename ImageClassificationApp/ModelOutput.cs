namespace ImageClassificationApp
{
    public class ModelOutput
    {
        public uint label { get; set; }

        public byte[] imageSource { get; set; }

        public string predictedLabel { get; set; }

        public float[] score { get; set; }

    }

}