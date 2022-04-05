using System.Collections.Generic;

namespace MediaLibrary
{
    public interface IRepository
    {
        void Write();
        void Read();
    }
}