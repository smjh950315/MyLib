namespace MyLib.Mvc
{
    public class PageHelper
    {
        public int Id { get; set; }
        public int PageSize { get; private set; }
        public PageIterator Page { get; private set; }
        public String? EntityNow { get; set; }
        public List<List<dynamic>> PageList { get; private set; }        
        public Number ItemCount { get; private set; }        
        private List<dynamic> ToPage()
        {
            return PageList[Page - 1];
        }
        public void ClearPageInfo()
        {
            PageList.Clear();
            Page=new PageIterator();
            ItemCount = 0;
        }
        public PageHelper()
        {
            SetPageSize(10);
            ItemCount = 0;
            Page = new PageIterator();
            PageList = new List<List<dynamic>>();
        }
        public PageHelper(int pageSize) : this()
        {
            SetPageSize(pageSize);
        }
        public PageHelper(int pageSize, dynamic itemList) : this(pageSize)
        {
            Load(itemList);
        }
        /// <summary>
        /// items = List of model
        /// </summary>
        /// <param name="items"></param>
        public void Load(dynamic? items)
        {
            if (items == null)
            {
                List<dynamic> list = new List<dynamic>();
                PageList.Add(list);
                return;
            }
            PageList = new List<List<dynamic>>();
            List<dynamic> itemInPage = new List<dynamic>();
            bool tried=false;
            if (!tried)
            {
                try
                {
                    foreach (var item in items)
                    {
                        itemInPage.Add(item);
                        if (itemInPage.Count >= PageSize)
                        {
                            PageList.Add(itemInPage);
                            itemInPage = new List<dynamic>();
                        }
                    }
                }
                catch
                {
                    tried = true;
                }
            }
            PageList.Add(itemInPage);
            Page.SetMax(PageList.Count);
        }
        public void SetPageSize(int? pageSize)
        {
            ItemCount = 0;
            if(pageSize != null)
            {
                PageSize = pageSize.Value;
            }
            else
            {
                PageSize = 10;
            }
        }
        public List<dynamic> ToPage(int? page)
        {
            Page.Set(page);
            return ToPage();
        }
        public List<dynamic> PrevPage()
        {
            return ToPage(Page--);
        }
        public List<dynamic> NextPage()
        {
            return ToPage(Page++);
        }
    }
    public class PageIterator
    {
        public int Prev { get; private set; }
        public int Now { get; private set; }
        public int Next { get; private set; }
        public int Max { get; private set; }
        private int Format(int? value)
        {
            if (value == null) { return 1; }
            if (value < 1) { value = 1; }
            if (value > Max) { value = Max; }
            return value.Value;
        }
        public void SetMax(int max)
        {
            Max = max < 1 ? 1 : max;
        }
        public PageIterator()
        {
            Prev = 1;
            Now = 1;
            Next = 1;
            Max = 1;
        }
        public PageIterator(int now, int max)
        {
            SetMax(max);
            Set(now);
        }
        public int Set(int? now)
        {
            Max = Format(Max);
            Now = Format(now);
            Prev = Format(Now - 1);
            Next = Format(Now + 1);
            return Now - 1;
        }
        public static PageIterator operator ++(PageIterator it)
        {
            return new PageIterator(it.Now++, it.Max);
        }
        public static PageIterator operator --(PageIterator it)
        {
            return new PageIterator(it.Now--, it.Max);
        }
        public static implicit operator int(PageIterator page)
        {
            return page.Now;
        }
    }
}
