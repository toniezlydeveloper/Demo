using Movement.Data;

namespace Movement.Composites
{
    public abstract class AComposite
    {
        public void Init(References references, Settings settings, State state)
        {
            Read(references);
            Read(settings);
            Read(state);
        }

        protected virtual void Read(References references)
        {
        }

        protected virtual void Read(Settings settings)
        {
        }

        protected virtual void Read(State state)
        {
        }
    }
}