namespace KeyboardEPCS
{
    public class ShrinkingArray<Element>
    {
        readonly Element[] data;
        int start;
        int length;

        public int Length { get { return length; } }

        public ShrinkingArray(Element[] data) { this.data = (Element[])data.Clone(); start = 0; length = data.Length; }

        public Element this[int index]
        {
            get
            {
                return data[start + index];
            }
            set
            {
                data[start + index] = value;
            }
        }

        public void Shorten() { start++; length--; }

        public void Shrink(int start) { this.start = this.start + start; length -= start; }
        public void Shrink(int start, int length) { this.start = this.start + start; this.length = length; }
    }
}
