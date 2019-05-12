namespace Deribit.S4KTNET.Core
{
    public interface IValidatable
    {
        // validate the object. Throw exceptions if object is invalid
        void Validate();
    }
}