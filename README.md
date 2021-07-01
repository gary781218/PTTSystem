# WinForm PTT系統複刻

這是筆者二個完成的專案, 是使用winform完成的, database以MSSQL實作, PTT相關資訊是以Selenium爬取[PTT網頁版](https://www.ptt.cc/bbs/hotboards.html)而取得，可使用guest登入或自行用new自行申請一組帳號。使用方法跟PTT相同，登入後自行挑類別、文章閱讀。

![image](https://user-images.githubusercontent.com/49896529/124077112-b0f9f000-da79-11eb-8210-d0c0c4d62785.png)

</br>

## 事前準備

1. 筆者有自行寫了建立帳號的功能，密碼的部分使用[BCrypt.Net-Next](https://www.nuget.org/packages/BCrypt.Net-Next/)進行加鹽雜湊
2. [CefSharp](https://www.nuget.org/packages/CefSharp.WinForms/)
3. 資料庫請自行使用PTTDatabase.sql中的Script自行建立，因資料龐大，筆者也只抓了一部分的資料作為測試，故後面幾個分類沒有文章是正常的，資料最後更新日期為2021/04。

</br>

程式邏輯如下

1. 在input內輸入關鍵字, 按下搜尋
2. 使用**GMap.NET.WinForms**進行一系列的認證與查詢
3. 依查詢結果筆數顯示於畫面
4. 對單一景點，點擊顯示地圖, 使用CefSharp顯示Google Map
5. 對單一景點，點擊加入資料庫，使用FileStream將景點資料寫入CSV

</br>

## 主程式

主程式碼為**LoginForm.cs**, 相關的儲存路徑寫在**App.config**, 使用者須自行更改路徑.

</br>

#### 亮點

1. 前端簡單驗證
2. 密碼加鹽雜湊
3. 模擬PTT的顏色及樣式
4. 手刻分頁器
5. 關鍵字查詢之AutoComplete
6. 將部分網址轉換為圖片或影片，要事先安裝CefSharp就是為了把網址轉換成可直接看的YouTube影片

#### 相關技術網誌

- [介紹網誌](https://dotblogs.com.tw/supergary/2021/01/20/project2_intro)