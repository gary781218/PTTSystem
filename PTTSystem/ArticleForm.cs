using CCWin;
using CCWin.Win32.Struct;
using CefSharp.DevTools.IndexedDB;
using PTTSystem.Component;
using PTTSystem.Function;
using PTTSystem.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace PTTSystem
{
    public partial class ArticleForm : Skin_Color
    {
        //共用參數區
        Rectangle screen;                  //螢幕設定
        Block block;                       //Block Form傳回的物件
        DataTable dt;
        DateConvert dataConvert;
        Account account;
        ArticleFLPModulation flowLayoutPanel1;
        List<Article> articles;
        FlowLayoutPannelModulation div;    //文章列表顯示區最外層容器，會因為頁數顯示內容不同，故拉出來提供給click事件共用

        NumberCheck check = new NumberCheck();

        //分頁器共用
        FlowLayoutPanel pageIndexChoose;    //分頁器容器(包含跳頁按鈕)
        FlowLayoutPanel indexPanel;        //分頁器的頁數容器
        Dictionary<string, int> page_dict;
        int pageIndex = 1;                 //目前所在頁數
        int totalPage = 1;                 //總頁數
        int initialArticleCount = 1;       //初始化時顯示多少文章
        int ArticesOnPage = 20;            //每頁最多顯示多少篇文章
        int paginationsOnFlp = 5;          //顯示幾個頁數按鈕出來
        ButtonModulation back_button;             //分頁倒退按鈕，為了製作click event拉出來
        ButtonModulation next_button;             //分頁前進按鈕，為了製作click event拉出來
        TextBox pagination_Tbox;                  //分頁OK按鈕，為了製作click event拉出來
        PaginationLabelModulation pagination_label1;
        PaginationLabelModulation pagination_label2;
        ButtonModulation go;

        private static System.Timers.Timer aTimer;

        public ArticleForm(Block block, Account account)
        {
            InitializeComponent();
            screen = Screen.FromControl(this).Bounds;
            dataConvert = new DateConvert();
            this.block = block;
            this.account = account;
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            flowLayoutPanel1 = new ArticleFLPModulation(textBox1.Location.X,
                                                         textBox1.Location.Y + textBox1.Height + 10,
                                                         textBox1.Width,
                                                         200,
                                                         true);
            flowLayoutPanel1.Visible = false;
            flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.Controls.Add(flowLayoutPanel1);

            aTimer = new System.Timers.Timer(500);
            aTimer.Elapsed += new ElapsedEventHandler(Run);
        }
        private void ArticleForm_Load(object sender, EventArgs e)
        {
            //Form視窗長寬高設定
            Width = dataConvert.SizeZoom(screen.Width, 0.9);
            Height = dataConvert.SizeZoom(screen.Height, 0.95);
            Location = new Point(25, 25);

            //textbox1設定
            textBox1.KeyDown += textBox1_Enter;
            textBox1.Width = Width-textBox1.Location.X-3;

            //bar長寬設定
            skinPanel1.Width = Width-5;
            skinPanel1.Height = 40;

            //顯示所在block
            BlockName_label.Text = block.name;

            //顯示頁數選擇器flp
            pageIndexChoose = new FlowLayoutPanel();
            pageIndexChoose.AutoSize = false;
            pageIndexChoose.BackColor = Color.FromArgb(68, 68, 68);
            pageIndexChoose.ForeColor = Color.FromArgb(221, 221, 221);
            pageIndexChoose.Size = new Size(93*5, 40);
            pageIndexChoose.Location = new Point(Width-93*5-2, 84);
            //pageIndexChoose.Location = new Point(1215, 84);
            this.Controls.Add(pageIndexChoose);

            //文章列表顯示區最外層容器
            div = new FlowLayoutPannelModulation(
                textBox1.Location.X,
                textBox1.Location.Y + 80,
                Width - 90,
                Height - 220,
                false
                );
            div.HorizontalScroll.Visible = false;

            //取得該版總文章數
            int articleCount = new Article().getArticleCount(block.block_ID.ToString());
            
            //計算分頁數、初始化顯示文章數
            page_dict = check.ArticlePagination(articleCount, ArticesOnPage);      //計算並取得分頁資訊
            totalPage = page_dict["pageCount"];                                    //取得分頁數量
            initialArticleCount = page_dict["lastCount"];                          //取得第一頁顯示筆數
            
            //建立分頁器
            back_button = new ButtonModulation("<",10, 0, 0, 30, 30, false, false);
            next_button = new ButtonModulation(">", 10, 0, 0, 30, 30, false, totalPage == 1 ? false : true);
            pagination_label1 = new PaginationLabelModulation("跳至第", 10, 0, 0, 50, 30, false);
            pagination_Tbox = new TextBox();
            pagination_Tbox.Size = new Size(40, 30);
            pagination_Tbox.Margin = new Padding(0, 8, 0, 0);
            pagination_label2 = new PaginationLabelModulation("頁", 10, 0, 0, 30, 30, false);
            go = new ButtonModulation("確定", 10, 0, 0, 50, 30, false, true);
            indexPanel = new FlowLayoutPanel();
            indexPanel.AutoSize = true;
            indexPanel.Margin = new Padding(0, 0, 0, 0);

            //綁定事件
            back_button.Click += new EventHandler(backPage_Click);
            next_button.Click += new EventHandler(nextPage_Click);
            go.Click += new EventHandler(GoToPage_Click);

            //判斷pagination要顯示的數量
            int btnCount = (totalPage > paginationsOnFlp) ? paginationsOnFlp : totalPage;
            
            //顯示文章至畫面中
            this.UpdatePaginationButton(pageIndex, btnCount, pageIndex);

            pageIndexChoose.Controls.Add(back_button);
            pageIndexChoose.Controls.Add(indexPanel);
            pageIndexChoose.Controls.Add(next_button);
            pageIndexChoose.Controls.Add(pagination_label1);
            pageIndexChoose.Controls.Add(pagination_Tbox);
            pageIndexChoose.Controls.Add(pagination_label2);
            pageIndexChoose.Controls.Add(go);

            //取得文章資料，並顯示
            (int, int) articleIndex = check.GetArticleIndexFromPage(pageIndex, initialArticleCount, ArticesOnPage, totalPage);  //以頁數轉換成db的index
            articles = new Article().getArticles(block.block_ID.ToString(), articleIndex.Item1, articleIndex.Item2);  //向資料庫取回資料
            showArticle(articles);
        }
        private void showArticle(List<Article> result)
        {
            //label column width setting
            int popWidth = dataConvert.SizeZoom(div.Width, 0.03);
            int titleWidth = dataConvert.SizeZoom(div.Width, 0.57);
            int authorWidth = dataConvert.SizeZoom(div.Width, 0.82);
            int dateWidth = dataConvert.SizeZoom(div.Width, 0.07);
            int dotWidth = dataConvert.SizeZoom(div.Width, 0.03);

            foreach (var article in result)
            {
                //人氣
                string popularty = article.popularity == null ? "" : article.popularity;
                ArticleLabelModulation popularity = new ArticleLabelModulation(popularty, 20, 0, 0, popWidth, 40, false);
                NumberCheck numberCheck = new NumberCheck();
                List<int> colorList = numberCheck.ArticlePopulartyColor(popularty);
                popularity.ForeColor = Color.FromArgb(colorList[0], colorList[1], colorList[2]);
                //標題
                ArticleLabelModulation title = new ArticleLabelModulation(article.article_title, 20, titleWidth, 0, 0, 40, true);
                //標題後
                //MessageBox.Show($"{div.Width} {popWidth} {title.Width}");
                ArticleLabelModulation title_space = new ArticleLabelModulation("", 20, 0, 0, div.Width - popWidth - title.Width - 300, 40, true);
                //作者前空白
                ArticleLabelModulation author_space = new ArticleLabelModulation("", 20, 0, 0, popWidth, 40, false);
                //作者
                ArticleLabelModulation author = new ArticleLabelModulation(article.user_ID, 20, 0, 0, authorWidth, 40, false);
                ArticleLabelModulation date = new ArticleLabelModulation(article.date, 20, 0, 1, dateWidth, 40, false);
                ArticleLabelModulation dot = new ArticleLabelModulation("...", 20, 0, 1, dotWidth, 40, false);

                ArticleFLPModulation tr = new ArticleFLPModulation(0, 0, div.Width, popularity.Height * 2, false);
                ArticleTrFLPModulation tr1 = new ArticleTrFLPModulation(0, 0, div.Width, popularity.Height, false);
                ArticleTrFLPModulation tr2 = new ArticleTrFLPModulation(0, 0, div.Width, popularity.Height, false);

                List<object> list = new List<object>();
                list.Add(block);
                list.Add(article);
                title.Tag = list;

                title.MouseHover += new EventHandler(CellMouseHover);
                title.MouseLeave += new EventHandler(CellMouseLeave);
                title.Click += new EventHandler(CellClick);

                tr1.Controls.Add(popularity);
                tr1.Controls.Add(title);
                tr1.Controls.Add(title_space);
                tr2.Controls.Add(author_space);
                tr2.Controls.Add(author);
                tr2.Controls.Add(date);
                tr2.Controls.Add(dot);
                tr.Controls.Add(tr1);
                tr.Controls.Add(tr2);
                div.Controls.Add(tr);
                this.Controls.Add(div);
            }
        }
        public void UpdatePaginationButton(int start_index, int button_count, int pageIndex)
        {
            
            for (int i = start_index; i < start_index + button_count; i++)
            {
                ButtonModulation index = new ButtonModulation(i.ToString(), 10, 0, 0, 30, 30, false, true);
                index.Click += new EventHandler(PageButton_Click);
                if (i == pageIndex)
                {
                    index.BackColor = Color.Silver;
                    index.ForeColor = Color.FromArgb(68, 68, 68);
                }
                else
                {
                    index.BackColor = Color.FromArgb(240);
                    index.ForeColor = Color.FromArgb(0);
                }
                indexPanel.Controls.Add(index);
            }
        }
        private void BlockNameLabelMouseHover(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        private void BlockNameLabelMouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }
        private void CellMouseHover(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.BackColor = Color.FromArgb(170, 170, 170);
            label.ForeColor = Color.FromArgb(17, 17, 17);
            Cursor = Cursors.Hand;
        }
        private void CellMouseLeave(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.BackColor = Color.FromArgb(17, 17, 17);
            label.ForeColor = Color.FromArgb(170, 170, 170);
            Cursor = Cursors.Default;
        }
        private void CellClick(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            List<object> list = (List<object>)label.Tag;
            ContextForm form = new ContextForm(list);
            form.ShowDialog(this);
        }
        private void back2Block_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            aTimer.Enabled = true;
            if (textBox1.Text.Equals(""))
            {
                flowLayoutPanel1.Visible = false;
            }
        }
        public void Run(object sender, ElapsedEventArgs e)
        {
            flowLayoutPanel1.Invoke(new EventHandler(delegate
            {
                Database db = new Database();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("userGUID", account.userGUID);
                dict.Add("start", 0);
                dict.Add("end", 5);
                dict.Add("word", "%" + textBox1.Text + "%");

                List<KeywordTable> result = db.query<KeywordTable>(@"Select * from KeywordTable
                                                                         where userGUID = @userGUID and isDelete = 'false' and keywordString like @word 
                                                                         order by createTime DESC
                                                                         offset @start rows fetch next @end rows only", dict);

                if (result.Count == 0)
                {
                    result = db.query<KeywordTable>(@"Select * from KeywordTable
                                                        where userGUID = @userGUID and isDelete = 'false' 
                                                        order by createTime DESC
                                                        offset @start rows fetch next @end rows only", dict);
                }

                if (result.Count != 0)
                {
                    flowLayoutPanel1.Controls.Clear();
                    foreach (var x in result)
                    {
                        Label label = new Label();
                        label.AutoSize = false;
                        label.Width = flowLayoutPanel1.Width;
                        label.Height = textBox1.Height;
                        label.Text = x.keywordString;
                        label.Click += label_click;
                        flowLayoutPanel1.Controls.Add(label);
                    }
                    flowLayoutPanel1.Visible = true;
                }
                aTimer.Enabled = false;
            }));
        }
        private void label_click(object sender, EventArgs e)
        {
            foreach (var x in flowLayoutPanel1.Controls)
            {
                Label labels = (Label)x;
                labels.BackColor = Color.White;
                labels.ForeColor = Color.Black;
            }

            Label label = (Label)sender;
            label.BackColor = Color.Blue;
            label.ForeColor = Color.White;
            textBox1.Text = label.Text;
            flowLayoutPanel1.Visible = false;
        }
        private void textBox1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals("搜尋文章..."))
            {
                textBox1.Text = "";
            }
        }
        private void backPage_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            pageIndex -= 1;

            //特效
            if (pageIndex == 1)
            {
                back_button.Enabled = false;
                next_button.Enabled = true;
            }
            else
            {
                back_button.Enabled = true;
                next_button.Enabled = true;
            }

            //判斷是否該更換indexPanel.Controls
            if (pageIndex >= paginationsOnFlp && pageIndex % paginationsOnFlp == 0)
            {
                indexPanel.Controls.Clear();
                if (pageIndex == paginationsOnFlp)
                {
                    UpdatePaginationButton(1, paginationsOnFlp, pageIndex);
                }
                else
                {
                    UpdatePaginationButton(pageIndex, paginationsOnFlp, pageIndex);
                }
            }
            else
            {
                foreach (Control control in indexPanel.Controls)
                {
                    control.BackColor = Color.FromArgb(240);
                    control.ForeColor = Color.FromArgb(0);
                    if (control.Text.Equals(pageIndex.ToString()))
                    {
                        control.BackColor = Color.Silver;
                        control.ForeColor = Color.FromArgb(68, 68, 68);
                    }
                }
            }

            //內容顯示
            div.Controls.Clear();
            (int, int) articleIndex = check.GetArticleIndexFromPage(pageIndex, initialArticleCount, ArticesOnPage, totalPage);
            articles = new Article().getArticles(block.block_ID.ToString(), articleIndex.Item1, articleIndex.Item2);
            showArticle(articles);
        }
        private void nextPage_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            pageIndex += 1;

            //特效
            if (pageIndex == totalPage)
            {
                back_button.Enabled = true;
                next_button.Enabled = false;
            }
            else
            {
                back_button.Enabled = true;
                next_button.Enabled = true;
            }


            //判斷是否該更換indexPanel.Controls
            if (pageIndex >= paginationsOnFlp && pageIndex % paginationsOnFlp == 1)
            {
                indexPanel.Controls.Clear();
                if (totalPage - pageIndex + 1 >= paginationsOnFlp)
                {
                    UpdatePaginationButton(pageIndex, paginationsOnFlp, pageIndex);
                }
                else 
                {
                    UpdatePaginationButton(pageIndex, totalPage-pageIndex+1, pageIndex);
                }
            }
            else
            {
                foreach (Control control in indexPanel.Controls)
                {
                    control.BackColor = Color.FromArgb(240);
                    control.ForeColor = Color.FromArgb(0);
                    if (control.Text.Equals(pageIndex.ToString()))
                    {
                        control.BackColor = Color.Silver;
                        control.ForeColor = Color.FromArgb(68, 68, 68);
                    }
                }
            }
            //foreach (Control control in indexPanel.Controls)
            //{
            //    control.BackColor = Color.FromArgb(240);
            //    control.ForeColor = Color.FromArgb(0);
            //    if (control.Text.Equals(pageIndex.ToString()))
            //    {
            //        control.BackColor = Color.Silver;
            //        control.ForeColor = Color.FromArgb(68, 68, 68);
            //    }
            //}

            //內容顯示
            div.Controls.Clear();
            (int, int) articleIndex = check.GetArticleIndexFromPage(pageIndex, initialArticleCount, ArticesOnPage, totalPage);
            articles = new Article().getArticles(block.block_ID.ToString(), articleIndex.Item1, articleIndex.Item2);
            showArticle(articles);
        }
        private void PageButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            pageIndex = int.Parse(button.Text);

            //外觀特效
            foreach (Control control in indexPanel.Controls)
            {
                control.BackColor = Color.FromArgb(240);
                control.ForeColor = Color.FromArgb(0);
            }

            button.BackColor = Color.Silver;
            button.ForeColor = Color.FromArgb(68, 68, 68);

            if (button.Text.Equals("1"))
            {
                back_button.Enabled = false;
                next_button.Enabled = true;

            }
            else if (button.Text.Equals(totalPage.ToString()))
            {
                back_button.Enabled = true;
                next_button.Enabled = false;
            }
            else
            {
                back_button.Enabled = true;
                next_button.Enabled = true;
            }
            

            //內容顯示
            div.Controls.Clear();
            (int, int) articleIndex = check.GetArticleIndexFromPage(pageIndex, initialArticleCount, ArticesOnPage, totalPage);
            articles = new Article().getArticles(block.block_ID.ToString(), articleIndex.Item1, articleIndex.Item2);                 
            showArticle(articles);
        }
        private void GoToPage_Click(object sender, EventArgs e)
        {
            int number;
            if (int.TryParse(pagination_Tbox.Text, out number))
            {
                if (number <= totalPage)
                {
                    pageIndex = number;

                    //上下頁按鈕防呆
                    if (pageIndex == 1)
                    {
                        back_button.Enabled = false;
                        next_button.Enabled = true;
                    }
                    else if (pageIndex == totalPage)
                    {
                        back_button.Enabled = true;
                        next_button.Enabled = false;
                    }
                    else
                    {
                        back_button.Enabled = true;
                        next_button.Enabled = true;
                    }

                    int start = 0;
                    int buttonCount = 0;
                    indexPanel.Controls.Clear();
                    if (pageIndex <= paginationsOnFlp) //index在第一頁
                    {
                        start = 1;
                        if (totalPage > paginationsOnFlp)
                        {
                            buttonCount = 5;
                        }
                        else
                        {
                            buttonCount = totalPage;
                        }
                        UpdatePaginationButton(1, buttonCount, pageIndex);
                    }
                    else //index在第二頁以上
                    {
                        start = pageIndex - (pageIndex % paginationsOnFlp) + 1;
                        if (totalPage - pageIndex < paginationsOnFlp)  //按鈕只到這頁
                        {
                            buttonCount = totalPage - start + 1;
                        }
                        else //後面還有一整頁N個按鈕
                        {
                            buttonCount = paginationsOnFlp;
                        }
                        UpdatePaginationButton(start, buttonCount, pageIndex);
                    }

                    //內容顯示
                    div.Controls.Clear();
                    (int, int) articleIndex = check.GetArticleIndexFromPage(pageIndex, initialArticleCount, ArticesOnPage, totalPage);
                    articles = new Article().getArticles(block.block_ID.ToString(), articleIndex.Item1, articleIndex.Item2);
                    showArticle(articles);
                }
                else
                {
                    MessageBox.Show($"總頁數共{totalPage}頁，請重新輸入");
                }
            }
        }
        private void textBox1_Enter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter )
            {
                flowLayoutPanel1.Visible = false;
                Database db = new Database();
                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("article_title", $"%{textBox1.Text}%");
                dict.Add("block_ID", block.block_ID);
                string sqlString = "select * from Article where article_title like @article_title and block_ID = @block_ID";
                List<Article> result = db.query<Article>(sqlString,dict);

                pageIndex = 1;
                if (result.Count == 0)
                {
                    MessageBox.Show("沒有符合查詢的結果");
                }
                else
                {
                    div.Controls.Clear();
                    page_dict = check.ArticlePagination(result.Count, ArticesOnPage);      //計算並取得分頁資訊
                    totalPage = page_dict["pageCount"];                                    //取得分頁數量
                    initialArticleCount = page_dict["lastCount"];                          //取得第一頁顯示筆數
                    //取得文章資料，並顯示
                    (int, int) articleIndex = check.GetArticleIndexFromPage(pageIndex, initialArticleCount, ArticesOnPage, totalPage);  //以頁數轉換成db的index
                    articles = result;
                    showArticle(articles);
                    indexPanel.Controls.Clear();

                    //調整分頁選
                    int start = 0;
                    int buttonCount = 0;
                    start = 1;
                    if (totalPage > paginationsOnFlp)
                    {
                        buttonCount = 5;
                    }
                    else
                    {
                        buttonCount = totalPage;
                    }
                    UpdatePaginationButton(1, buttonCount, pageIndex);

                    //將使用者-關鍵字存到資料庫
                    dict.Clear();
                    dict.Add("keywordString", textBox1.Text);
                    sqlString = "select count(*) from KeywordTable where keywordString = @keywordString";
                    int keywordCount = db.queryBaseType<int>(sqlString, dict)[0];
                    if (keywordCount == 0)
                    {
                        KeywordTable keywordModel = new KeywordTable();
                        keywordModel.userGUID = account.userGUID;
                        keywordModel.keywordGUID = Guid.NewGuid();
                        keywordModel.keywordString = textBox1.Text;
                        keywordModel.createTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        keywordModel.isDelete = false;
                        db.insert(keywordModel);
                    }
                }
            }
        }

        private void BlockName_label_Click(object sender, EventArgs e)
        {
            //取得該版總文章數
            int articleCount = new Article().getArticleCount(block.block_ID.ToString());
            //計算分頁數、初始化顯示文章數
            page_dict = check.ArticlePagination(articleCount, ArticesOnPage);      //計算並取得分頁資訊
            totalPage = page_dict["pageCount"];                                    //取得分頁數量
            initialArticleCount = page_dict["lastCount"];                          //取得第一頁顯示筆數

            int start = 0;
            int buttonCount = 0;
            indexPanel.Controls.Clear();
            if (pageIndex <= paginationsOnFlp) //index在第一頁
            {
                start = 1;
                if (totalPage > paginationsOnFlp)
                {
                    buttonCount = 5;
                }
                else
                {
                    buttonCount = totalPage;
                }
                UpdatePaginationButton(1, buttonCount, pageIndex);
            }
            else //index在第二頁以上
            {
                start = pageIndex - (pageIndex % paginationsOnFlp) + 1;
                if (totalPage - pageIndex < paginationsOnFlp)  //按鈕只到這頁
                {
                    buttonCount = totalPage - start + 1;
                }
                else //後面還有一整頁N個按鈕
                {
                    buttonCount = paginationsOnFlp;
                }
                UpdatePaginationButton(start, buttonCount, pageIndex);
            }

            //取得文章資料，並顯示
            div.Controls.Clear();
            pageIndex = 1;
            (int, int) articleIndex = check.GetArticleIndexFromPage(pageIndex, initialArticleCount, ArticesOnPage, totalPage);  //以頁數轉換成db的index
            articles = new Article().getArticles(block.block_ID.ToString(), articleIndex.Item1, articleIndex.Item2);            //向資料庫取回資料
            showArticle(articles);
        }

        private void 新增文章ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateArticle form = new CreateArticle(block,account);
            form.ShowDialog(this);
        }
    }
}
