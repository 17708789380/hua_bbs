=%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="WebApplication.common.Top" %>
=-
<!DOCTYPE html>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>Insert title here</title>
<link href="/css/style.css" rel="stylesheet" />
<script type="text/javascript">
function logout() {
	if (confirm("您确定要退出系统吗？")) {
	    window.location.href = "/admin/Logout.aspx";
	}
}
function login(){
	var curPage=window.location.href;
	window.location.href = "/admin/Login.aspx?prePage=" + curPage;
}
function checkUserLogin() {

    //if (Session["Username_session"] == null) {
    //    alert("您还未登陆！");
    //    window.location.href = "/admin/Login.aspx";
    //}

    window.location.href = "/userCenter/UserInfo.aspx";
}
</script>
	
<style type="text/css">
*{
	padding:0;
	margin:0;
}

/*ul设置*/

#menu .menu_ul2{
	width:200px;
    margin-right:-500px;
	display:inline;
	float:right;
	height:70px;
	margin-top:12px;
}
/*li设置*/
/*.menu_li1_pp{
	color:white;
	font-size:33px;
}*/
.enu_ul2m{
    /*display:inline-block;*/
    margin-top:35px;
}
.enu_ul2m li{
    display:none;
    display:inline-block;
    margin-left:16px;
    color:white;
}
/*.menu_li1 .menu_li2{
	height:100px;
	line-height:100px;
}*/

.menu_li3{
    /*margin-left:-150px;*/
}
.menu_li2 a{
	color:white;
	/*margin-left:5px;*/
    font-size:18px;
}
/*悬浮设置*/
.menu_li2 ul li:hover{
	color:red;
    
}
/*轮播图片*/
.img{
	width:100%;
	height:350px;
    
}

/*
        html {
            height: 100%;
        }

        body {
            background: #f0f3ef;
            height: 100%;
        }*/
#menu-wrapper{
    display: flex;
}
        .container {
            width:400px;
            height:100px;
            line-height:100px;
           
        }

        .bgDiv {
            width:400px;         
            box-sizing: border-box;
            width: 500px;
            height: 100px;
            line-height:100px;
            /*position: relative;*/
            /* position: absolute;
            left: 50%;
            top: 50%;
            transform: translate(-50%, -50%); */
        }

        .search-input-text {
            border: 1px solid #b6b6b6;
            width: 200px;
            background: #fff;
            height: 33px;
            line-height: 33px;
            font-size: 18px;
            padding: 3px 0 0 7px;
            margin-left:-130px;
        }

        .search-input-button {
            width: 90px;
            height: 30px;
            color: #fff;
            border-radius:5px;
            font-size: 16px;
            letter-spacing: 3px;
            background: #3385ff;
            border: .5px solid #2d78f4;
            margin-left: -5px;
            vertical-align: top;
            opacity: .9;
            margin-top:32px;
            margin-left:0.5px;
        }

        .search-input-button:hover {
            opacity: 1;
            box-shadow: 0 1px 1px #333;
            cursor: pointer;
        }

        .suggest {
            width: 400px;
            margin-left:52px;
            /*position: absolute;*/
            /*border: 1px solid #999;*/
            background: #fff;
            display: none;
            float:left;
        }

        .suggest ul {
            list-style: none;
            margin: 0;
            padding: 0;
            text-align:left;
        }

        .suggest ul li {
            padding: 3px;
            font-size: 17px;
            line-height: 25px;
            cursor: pointer;
        }

        .suggest ul li:hover {
            background-color: #e5e5e5
        }
        .font{ margin-top:20px;margin-left:30px;}
        .font-1{color:white;font-size:81px;font-family:'STXinwei';margin-left:30px}
        .font-2{color:white;font-size:40px;font-family:'STXinwei';margin-left:60px}
        

        #header-wrapper{
            /*position:relative;*/
            
        }

        .suggest{position:absolute;top:60px;left:300px;z-index:9999}
        
         #menu ul li a{border-bottom:2px solid #2F94A8}
        #menu ul li a:hover{color:red;border-bottom:2px solid red}

       
    </style>
</head>
<body>
<div>
	<div id="menu-wrapper">
        <div class="font">
                <span class="font-1">盼</span><br /><br />
                 <span class="font-2">盼</span>
        </div>
		   

    <div class="container">
        <div class="bgDiv">
        <input type="text" class="search-input-text" value="" autofocus placeholder="关键词">
        <input type="button" value="搜索一下" class="search-input-button" id="btn">
        <div class="suggest">
            <ul id="search-result">
            </ul>
        </div>
    </div>
</div>

<script>
    var suggestContainer = document.getElementsByClassName("suggest")[0];
    var searchInput = document.getElementsByClassName("search-input-text")[0];
    var bgDiv = document.getElementsByClassName("bgDiv")[0];
    var searchResult = document.getElementById("search-result");

    // 清除建议框内容
    function clearContent() {
        var size = searchResult.childNodes.length;
        for (var i = size - 1; i >= 0; i--) {
            searchResult.removeChild(searchResult.childNodes[i]);
        }
    };

    var timer = null;
    // 注册输入框键盘抬起事件
    searchInput.onkeyup = function (e) {
        suggestContainer.style.display = "block";
        // 如果输入框内容为空 清除内容且无需跨域请求
        if (this.value.length === 0) {
            clearContent();
            return;
        }
        if (this.timer) {
            clearTimeout(this.timer);
        }
        if (e.keyCode !== 40 && e.keyCode !== 38) {
            // 函数节流优化
            this.timer = setTimeout(() => {
                // 创建script标签JSONP跨域
                var script = document.createElement("script");
                script.src = "https://www.baidu.com/su?&wd=" + encodeURI(this.value.trim()) +
                    "&p=3&cb=handleSuggestion";
                document.body.appendChild(script);
            }, 130)
        }

    };

    // 回调函数处理返回值
    function handleSuggestion(res) {
        // 清空之前的数据！！
        clearContent();
        var result = res.s;
        // 截取前五个搜索建议项
        if (result.length > 4) {
            result = result.slice(0, 5)
        }
        for (let i = 0; i < result.length; i++) {
            // 动态创建li标签
            var liObj = document.createElement("li");
            liObj.innerHTML = result[i];
            searchResult.appendChild(liObj);
        }
        // 自执行匿名函数--删除用于跨域的script标签
        (function () {
            var s = document.querySelectorAll('script');
            for (var i = 1, len = s.length; i < len; i++) {
                document.body.removeChild(s[i]);
            }
        })()
    }


    function jumpPage() {
        window.open(`https://www.baidu.com/s?word=${encodeURI(searchInput.value)}`);
    }

    // 事件委托 点击li标签或者点击搜索按钮跳转到百度搜索页面
    bgDiv.addEventListener("click", function (e) {
        if (e.target.nodeName.toLowerCase() === 'li') {
            var keywords = e.target.innerText;
            searchInput.value = keywords;
            jumpPage();
        } else if (e.target.id === 'btn') {
            jumpPage();
        }
    }, false);

    var i = 0;
    var flag = 1;

    // 事件委托 监听键盘事件
    bgDiv.addEventListener("keydown", function (e) {
        var size = searchResult.childNodes.length;
        if (e.keyCode === 13) {
            jumpPage();
        };
        // 键盘向下事件
        if (e.keyCode === 40) {
            if (flag === 0) {
                i = i + 2;
            }
            flag = 1;
            e.preventDefault();
            if (i >= size) {
                i = 0;
            }
            if (i < size) {
                searchInput.value = searchResult.childNodes[i++].innerText;
            }
        };
        // 键盘向上事件
        if (e.keyCode === 38) {
            if (flag === 1) {
                i = i - 2;
            }
            flag = 0;
            e.preventDefault();
            if (i < 0) {
                i = size - 1;
            }
            if (i > -1) {
                searchInput.value = searchResult.childNodes[i--].innerText;
            }
        };
    }, false);

    // 点击页面任何其他地方 搜索结果框消失
    document.onclick = () => clearContent()


</script>


        <div id="menu" class="container-2" "> 
			<ul class="enu_ul2m">
				<li class="menu_li2 menu_li3" ><a  href="javascript:login()">登录</a></li>
				<li class="menu_li2">|</li>
				<li class="menu_li2"><a href="/admin/Register.aspx">注册</a></li>
				<li class="menu_li2">|</li>
				<li class="menu_li2"><a href="/admin/Login.aspx">个人中心</a></li>
			</ul>
            <hr / class="hr_1" style="width:360px;margin-left:170px">
		</div>
		<!-- end #menu --> 
	</div>

	<!-- 轮播图 -->
	<div id="header-wrapper" style="width:100%;height:330px;">
		

		<div id="myCarousel" class="carousel slide">
    <!-- 轮播（Carousel）指标 -->
    <ol class="carousel-indicators">
        <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
        <li data-target="#myCarousel" data-slide-to="1"></li>
        <li data-target="#myCarousel" data-slide-to="2"></li>
		<li data-target="#myCarousel" data-slide-to="3"></li>
		<li data-target="#myCarousel" data-slide-to="4"></li>
    </ol>   
    <!-- 轮播（Carousel）项目 -->
    <div class="carousel-inner">
        <div class="item active">
            <img class="img" src="../images/bj2.jpg" / alt="First slide">
        </div>
        <div class="item">
            <img class="img" src="../images/bj5.jpg" / alt="Second slide">
        </div>
        <div class="item">
            <img class="img" src="../images/bj6.jpg" / alt="Third slide">
        </div>
		<div class="item">
            <img class="img" src="../images/bj7.jpg" / alt="Third slide">
        </div>
		<div class="item">
            <img class="img" src="../images/bj1.jpg" / alt="Third slide">
        </div>
    </div>
    <!-- 轮播（Carousel）导航 -->
  
        <a class="carousel-control left" href="#myCarousel" 
       data-slide="prev"> <span _ngcontent-c3="" aria-hidden="true" class="glyphicon glyphicon-chevron-right"></span></a>
    <a class="carousel-control right" href="#myCarousel" 
       data-slide="next">&rsaquo;</a>
</div>


		<%--<div id="header" class="container">--%>
			
			<%--<div id="logo">
				<h2><a href="#">盼盼论坛交流社区</a></h2>
				<p> <a href="#" rel="nofollow">盼盼论坛</a></p>
			</div>--%>
		<%--</div>--%>
	</div>

<div style="margin: 0 auto;margin-top:23px" align="right">
    
		<%--<c:choose>
			<c:when test="${not empty currentUser }">
				当前用户：<a href="#">${currentUser.nickName }</a>&nbsp;『<c:choose>
					                  	  		<c:when test="${currentUser.sectionList.size()==0&&currentUser.type!=2 }">
					                  	  			<font style="color: black;">普通用户</font>	
					                  	  		</c:when>
					                  	  		<c:when test="${currentUser.sectionList.size()!=0&&currentUser.type!=2 }">
					                  	  			<font style="color: blue;">版主</font>
					                  	  		</c:when>
					                  	  		<c:otherwise>
					                  	  			<font style="color: red;">管理员</font>
					                  	  		</c:otherwise>
					                  	  	</c:choose>』|
				<a href="javascript:logout()">注销</a>|
				<a href="/admin/Register.aspx">注册</a>|
				<a href="javascript:checkUserLogin()">个人中心</a>
			</c:when>
			<c:otherwise>
				<!-- <a href="login.jsp">登录</a>| -->
				<a href="javascript:login()">登录</a>|
				<a href="/admin/Register.aspx">注册</a>|
				<a href="javascript:checkUserLogin()">个人中心</a>
			</c:otherwise>
		</c:choose>--%>

    <%--未登录显示--%>
    <%if (Session["user"] == null)  //Session[]为全局变量, 无需定义，直接使用  
        {
          
            %>
	<%--<div id="middle"  style="height:45px;line-height:45px;background-color:#a7cfd5">
            <a style="font-size:25px;margin-right:20px;" href="javascript:login()"><b>登录</b></a>|
			<a style="font-size:25px;margin-right:20px" href="/admin/Register.aspx"><b>注册</b></a>|
			<a style="font-size:25px;margin-right:20px" href="/admin/Login.aspx"><b>个人中心</b></a>
		</div>--%>
      <%}
          else   //已登录显示
          {
              //如果已经登录则把登录、注册、个人中心隐藏
              Response.Write("<script>document.getElementById('menu').style.visibility='hidden'</script>");
              %>
            当前用户：<a href="#"><%=((hua_bbs.Model.User)Session["user"]).nickname %></a>&nbsp;『  
    
     <%     //三种身份登录
             if(((hua_bbs.Model.User)Session["user"]).type == "1")
             {
                 Response.Write("<font style='color: black;'>普通用户</font>	");
             }
             else if(((hua_bbs.Model.User)Session["user"]).type == "2")
             {
                 Response.Write("<font style='color: blue;'>版主</font>");
             }
             else
             {
                 Response.Write("<font style='color: red;'>管理员</font>");
             }

             Response.Write("』|<a href='javascript:logout()'>注销</a>|<a href='/admin/Register.aspx'>注册</a>|<a href='javascript:checkUserLogin()'>个人中心</a>");
         } %>
	</div>
</div>
</body>
</html>
