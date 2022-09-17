using MyLib;

namespace Godch
{
    public static class ReloadHelper
    {
        public static bool? NeedReload(Number id1, ref Number id1Storage)
        {
            if (id1.IsNull && id1Storage.IsNull) { return null; }
            if (id1.IsNull) { return false; }
            else { id1Storage = id1; return id1Storage.IsChanged; }
        }
        public static bool? NeedReload(Number id1, Number id2, ref Number id1Storage)
        {
            if (id1.IsNull && id1Storage.IsNull) { return null; }
            if (!id1.IsNull) /* 則 id1Storage = 任意 */
            {
                id1Storage = id1;
            }
            if (id2.IsNull || id1Storage.IsChanged)
            {
                return true;
            }
            return false;
        }
        public static bool? NeedReload(Number id1, Number id2, ref Number id1Storage, dynamic? model)
        {
            if (id1.IsNull && id1Storage.IsNull) { return null; }
            if (!id1.IsNull)
            {
                id1Storage = id1;
            }

            if (id2.IsNull || id1Storage.IsChanged || model == null)
            {
                return true;
            }
            return false;
        }

    }
}
