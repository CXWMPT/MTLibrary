using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MTLibrary
{
    public partial class MTUC_Page_V1 : UserControl
    {
        /// <summary>
        /// 委托事件
        /// </summary>
        public event EventHandler EventHandler;
        #region 构造函数

        private bool LoadInit = false;
        public MTUC_Page_V1()
        {
            InitializeComponent();

            ArrayList listCmbVo = new ArrayList();

            listCmbVo.Add(new DictionaryEntry(10, "10 条/页"));
            listCmbVo.Add(new DictionaryEntry(20, "20 条/页"));
            listCmbVo.Add(new DictionaryEntry(30, "30 条/页"));
            listCmbVo.Add(new DictionaryEntry(50, "50 条/页"));
            listCmbVo.Add(new DictionaryEntry(100, "100 条/页"));

            CmbLimits.DataSource = listCmbVo; //绑定数据                  
            CmbLimits.DisplayMember = "Value";        //显示
            CmbLimits.ValueMember = "key";        //值
            CmbLimits.SelectedIndex = CmbLimits.Items.Count > 0 ? 0 : -1;
            PageSize = (int)CmbLimits.SelectedValue;
            LoadInit = true;
        }

        #endregion
        #region 分页字段和属性
        private int currentTempPageIndex = 0;
        private int currentPageIndex = 1;

        /// <summary>
        /// 当前页数
        /// </summary>
        public virtual int CurrentPageIndex
        {
            get { return currentPageIndex; }
            set
            {
                currentPageIndex = value;

            }
        }

        private  int pageSize = 10;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public virtual int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public  void InitPageSizeValue(int i)
        {
            LoadInit = false;
            CmbLimits.SelectedIndex = CmbLimits.Items.Count > 0 ? 0 : -1;
            CmbLimits.SelectedValue = i;
            PageSize = (int)CmbLimits.SelectedValue;
            LoadInit = true;
        }


        public int totalCount = 0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public virtual int TotalCount
        {
            get { return totalCount; }
            set
            {
                totalCount = value;

            }
        }
        private int totalPageCount = 0;
        /// <summary>
        /// 分页总页数
        /// </summary>
        public virtual int TotalPageCount
        {
            get { return totalPageCount; }
            set
            {
                totalPageCount = value;

            }
        }

        private int pageCount = 0;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                //if (pageSize != 0)
                //{
                //    pageCount = GetPageCount();
                //}
                return pageCount;
            }
        }
        #endregion

        public void SetCurrentPageIndexAndTotalCount(int currentPageIndex = 0, int totalCount = 0)
        {
            this.CurrentPageIndex = currentPageIndex;
            this.TotalCount = totalCount;
            ControlRecordCount.Text = "共有" + TotalCount.ToString() + "条记录";
            if (TotalCount % PageSize == 0)
            {
                TotalPageCount = TotalCount / PageSize;
            }
            else
            {
                TotalPageCount = TotalCount / PageSize + 1;
            }

            ControlPageInfo.Text = CurrentPageIndex + " / " + TotalPageCount;
        }
        public void RefreshPageControl()
        {
            ControlRecordCount.Text = "共有" + TotalCount.ToString() + "条记录";

            if (TotalCount <= 0)
            {
                ControlPageInfo.Text = "0/0";
                JumpPageValue.Text = "0";
            }
            else
            {
                if (TotalCount % PageSize == 0)
                {
                    TotalPageCount = TotalCount / PageSize;
                }
                else
                {
                    TotalPageCount = TotalCount / PageSize + 1;
                }
                ControlPageInfo.Text = CurrentPageIndex + " / " + TotalPageCount;
                JumpPageValue.Text = CurrentPageIndex.ToString();
            }
          
        }

        /// <summary>
        /// 第一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HomePage_Click(object sender, EventArgs e)
        {
            if (TotalCount <= 0)
            {
                this.EventHandler(this, new BaseEvent<string>(-1, "暂无数据"));
                return;
            }
            if (CurrentPageIndex == 1)
            {
                this.EventHandler(this, new BaseEvent<string>(-1, "当前已是第一页"));
            }
            else
            {
                CurrentPageIndex = 1;
                this.EventHandler(this, new BaseEvent<string>());
            }
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrevPage_Click(object sender, EventArgs e)
        {
            if (TotalCount <= 0)
            {
                this.EventHandler(this, new BaseEvent<string>(-1, "暂无数据"));
                return;
            }
            if (CurrentPageIndex > 1)
            {
                CurrentPageIndex--;
                this.EventHandler(this, new BaseEvent<string>());
            }
            else
            {
                this.EventHandler(this, new BaseEvent<string>(-1, "当前已是第一页"));
            }

          
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextPage_Click(object sender, EventArgs e)
        {
            if (TotalCount <= 0)
            {
                this.EventHandler(this, new BaseEvent<string>(-1, "暂无数据"));
                return;
            }
            if (CurrentPageIndex < TotalPageCount)
            {
                CurrentPageIndex++;
                this.EventHandler(this, new BaseEvent<string>());
            }
            else
            {
                this.EventHandler(this, new BaseEvent<string>(-1, "当前已是最后一页"));
            }

          
        }
        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LastPage_Click(object sender, EventArgs e)
        {
            if (TotalCount <= 0)
            {
                this.EventHandler(this, new BaseEvent<string>(-1, "暂无数据"));
                return;
            }
            if (CurrentPageIndex== TotalPageCount)
            {
                this.EventHandler(this, new BaseEvent<string>(-1, "当前已是最后一页"));
            }
            else
            {
                CurrentPageIndex = TotalPageCount;
                this.EventHandler(this, new BaseEvent<string>());
            }
        }

        /// <summary>
        /// 跳转数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Jump_Click(object sender, EventArgs e)
        {
            if (TotalCount <= 0)
            {
                this.EventHandler(this, new BaseEvent<string>(-1, "暂无数据"));
                return;
            }
            currentTempPageIndex = Conversion(JumpPageValue.Text);
            if (currentTempPageIndex > 0 && currentTempPageIndex <= TotalPageCount)
            {
                CurrentPageIndex = currentTempPageIndex;
                this.EventHandler(this, new BaseEvent<string>());
            }
            else
            {
                JumpPageValue.Text = CurrentPageIndex.ToString();
                this.EventHandler(this, new BaseEvent<string>(-1, "输入页数错误"));

            }
        }
 

        /// <summary>
        /// 切换分页数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Limits_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (LoadInit)
                {
                    ComboBox c = (ComboBox)sender;
                    PageSize = (int)c.SelectedValue;
                    CurrentPageIndex = 1;
                    this.EventHandler(this, new BaseEvent<string>());
                }
            }
            catch (Exception)
            {

            }
        }


        public static int Conversion(object o)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(o?.ToString())) return 0;
                return Convert.ToInt32(o);
            }
            catch
            {
                return 0;
            }
        }

    }
}
