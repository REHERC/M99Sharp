using M99Sharp.M99.Models;

namespace M99Sharp.M99.Systems.Classifier
{
    public sealed class ClassifierToken : GenericToken<ClassifierType, char>
    {
        public static ClassifierToken Empty => new ClassifierToken(FilePosition.Empty, ClassifierType.NONE, '\0');

        public ClassifierToken(FilePosition fp, ClassifierType ct, char c) : base(fp, ct, c)
        {

        }
    }
}
