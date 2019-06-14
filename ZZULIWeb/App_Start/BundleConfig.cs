using System.Web;
using System.Web.Optimization;

namespace ZZULIWeb
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            //网站首页
            bundles.Add(new ScriptBundle("~/index_js").Include("~/Scripts/index_showNav.js", "~/Scripts/jquery-3.1.1.js", "~/Scripts/search.js", "~/Scripts/float_top.js","~/Scripts/common.js"));
            bundles.Add(new StyleBundle("~/index_css").Include("~/Content/index.css", "~/Content/common.css"));
            //课程列表页
            bundles.Add(new StyleBundle("~/courselist_css").Include("~/Content/particulars.css", "~/Content/index.css", "~/Content/common.css", "~/Content/search.css"));
            bundles.Add(new ScriptBundle("~/courselist_js").Include("~/Scripts/jquery-3.1.1.js", "~/Scripts/float_top.js", "~/Scripts/search.js", "~/Scripts/common.js"));
            //课程目录
            bundles.Add(new StyleBundle("~/coursesection_css").Include("~/Content/common.css", "~/Content/class.css", "~/Content/index.css", "~/Content/search.css", "~/Content/alert.css"));
            bundles.Add(new ScriptBundle("~/coursesection_js").Include("~/Scripts/jquery-1.10.2.min.js", "~/Scripts/like.js", "~/Scripts/collect.js", "~/Scripts/section.js", "~/Scripts/search.js", "~/Scripts/alert.js", "~/Scripts/common.js"));
            //课程详情
            bundles.Add(new ScriptBundle("~/class_js").Include("~/Scripts/jquery-1.10.2.min.js", "~/Scripts/search.js", "~/Scripts/alert.js", "~/Scripts/like.js", "~/Scripts/collect.js", "~/Scripts/fold.js", "~/Scripts/common.js"));
            bundles.Add(new StyleBundle("~/class_css").Include("~/Content/index.css", "~/Content/class.css", "~/Content/common.css", "~/Content/search.css", "~/Content/alert.css"));

            //课程学习
            bundles.Add(new StyleBundle("~/video_css").Include("~/Content/video-js.css", "~/Content/index.css", "~/Content/common.css", "~/Content/video.css", "~/Content/alert.css"));
            bundles.Add(new ScriptBundle("~/video_js").Include("~/Scripts/video.js", "~/Scripts/jquery-3.1.1.js", "~/Scripts/jquery.unobtrusive-ajax.min.js", "~/Scripts/alert.js", "~/Scripts/ajax_goto_tab.js", "~/Scripts/common.js"));



            //笔记列表
            bundles.Add(new StyleBundle("~/notelist_css").Include("~/Content/index.css", "~/Content/notes.css", "~/Content/common.css", "~/Content/search.css", "~/Content/alert.css"));
            bundles.Add(new ScriptBundle("~/notelist_js").Include("~/Scripts/jquery-3.1.1.js", "~/Scripts/search.js", "~/Scripts/common.js"));

            //笔记详情
            bundles.Add(new StyleBundle("~/notedes_css").Include("~/Content/notes_des.css", "~/Content/index.css", "~/Content/userinfo.css", "~/Content/common.css", "~/Content/alert.css"));
            bundles.Add(new ScriptBundle("~/notedes_js").Include("~/Scripts/jquery-3.1.1.js", "~/Scripts/search.js", "~/Scripts/other.js", "~/Scripts/nicEdit.js", "~/Scripts/like.js", "~/Scripts/collect.js", "~/Scripts/submit_notecomment.js", "~/Scripts/alert.js", "~/Scripts/common.js"));
            //笔记发布

            bundles.Add(new StyleBundle("~/notesave_css").Include("~/Content/index.css", "~/Content/font.css", "~/Content/common.css", "~/Content/alert.css", "~/Content/wangEditor.min.css"));
            bundles.Add(new ScriptBundle("~/notesave_js").Include("~/Scripts/index.js", "~/Scripts/jquery-3.1.1.js", "~/Scripts/other.js", "~/Scripts/nicEdit.js", "~/Scripts/submit_note.js", "~/Scripts/alert.js", "~/Scripts/wangEditor.min.js", "~/Scripts/common.js"));


            //问答
            bundles.Add(new StyleBundle("~/questionlist_css").Include("~/Content/index.css", "~/Content/question.css", "~/Content/common.css", "~/Content/search.css", "~/Content/alert.css"));
            bundles.Add(new ScriptBundle("~/questionlist_js").Include("~/Scripts/jquery-3.1.1.js", "~/Scripts/search.js", "~/Scripts/question.js", "~/Scripts/alert.js", "~/Scripts/common.js"));

            //问题详情
            bundles.Add(new StyleBundle("~/questiondes_css").Include("~/Content/index.css", "~/Content/question_des.css", "~/Content/common.css", "~/Content/userinfo.css", "~/Content/alert.css"));
            bundles.Add(new ScriptBundle("~/questiondes_js").Include("~/Scripts/index.js", "~/Scripts/other.js", "~/Scripts/jquery-3.1.1.js", "~/Scripts/submit_questionanswer.js", "~/Scripts/search.js", "~/Scripts/like.js", "~/Scripts/collect.js","~/Scripts/alert.js"));

            //问题发布
            bundles.Add(new StyleBundle("~/questionsave_css").Include("~/Content/index.css", "~/Content/font.css", "~/Content/common.css", "~/Content/alert.css", "~/Content/wangEditor.min.css"));
            bundles.Add(new ScriptBundle("~/questionsave_js").Include("~/Scripts/jquery-3.1.1.js", "~/Scripts/other.js", "~/Scripts/search.js", "~/Scripts/index.js", "~/Scripts/nicEdit.js", "~/Scripts/submit_question.js", "~/Scripts/alert.js", "~/Scripts/wangEditor.min.js", "~/Scripts/common.js"));

            //用户信息
            bundles.Add(new StyleBundle("~/user_css").Include("~/Content/index.css", "~/Content/person.css", "~/Content/common.css", "~/Content/search.css", "~/Content/question.css", "~/Content/person_notes.css"));
            bundles.Add(new ScriptBundle("~/user_js").Include("~/Scripts/jquery-3.1.1.js", "~/Scripts/jquery-1.10.2.min.js", "~/Scripts/jquery.unobtrusive-ajax.min.js", "~/Scripts/search.js", "~/Scripts/user.js", "~/Scripts/ajax_goto_tab.js", "~/Scripts/common.js"));
            //用户登录
            bundles.Add(new StyleBundle("~/login_css").Include("~/Content/test.css", "~/Content/common.css", "~/Content/alert.css"));
            bundles.Add(new ScriptBundle("~/login_js").Include("~/Scripts/jquery-1.10.2.min.js", "~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.validate.unobtrusive.min.js", "~/Scripts/login.js", "~/Scripts/alert.js"));

            //注册
            bundles.Add(new StyleBundle("~/register_css").Include("~/Content/register.css", "~/Content/common.css", "~/Content/alert.css"));
            bundles.Add(new ScriptBundle("~/register_js").Include("~/Scripts/register.js", "~/Scripts/jquery-1.10.2.min.js", "~/Scripts/alert.js", "~/Scripts/jquery.validate.min.js", "~/Scripts/jquery.validate.unobtrusive.min.js"));
            //搜索
            bundles.Add(new StyleBundle("~/search_css").Include("~/Content/index.css", "~/Content/search.css", "~/Content/common.css"));
            bundles.Add(new ScriptBundle("~/search_js").Include("~/Scripts/jquery-3.1.1.js", "~/Scripts/search.js", "~/Scripts/userinfo.js", "~/Scripts/common.js"));

            //推荐
            bundles.Add(new StyleBundle("~/recommend_css").Include("~/Content/person.css", "~/Content/question.css", "~/Content/recommend.css", "~/Content/common.css", "~/Content/index.css", "~/Content/search.css"));
            bundles.Add(new ScriptBundle("~/recommend_js").Include("~/Scripts/jquery-3.1.1.js", "~/Scripts/search.js", "~/Scripts/common.js"));

            //上传下载
            bundles.Add(new StyleBundle("~/updown_css").Include("~/Content/index.css", "~/Content/download.css", "~/Content/common.css", "~/Content/search.css", "~/Content/alert.css"));
            bundles.Add(new ScriptBundle("~/updown_js").Include("~/Scripts/jquery-3.1.1.js", "~/Scripts/common.js", "~/Scripts/search.js", "~/Scripts/jquery-1.10.2.min.js", "~/Scripts/listbox.js", "~/Scripts/jquery.unobtrusive-ajax.min.js", "~/Scripts/alert.js", "~/Scripts/uploadfile.js", "~/Scripts/common.js"));

            ////搜索 课程
            //bundles.Add(new StyleBundle("~/searchcourse_css").Include("~/Content/index.css", "~/Content/search.css", "~/Content/search.css", "~/Content/userinfo.css", "~/Content/common.css"));
            //bundles.Add(new ScriptBundle("~/searchcourse_js").Include("~/Scripts/search.js", "~/Scripts/common.js", "~/Scripts/jquery-3.1.1.js"));
            //后台管理
            bundles.Add(new StyleBundle("~/al_css").Include("~/Content/behind.css", "~/Content/common.css"));
            bundles.Add(new ScriptBundle("~/al_js").Include("~/Scripts/behid.js", "~/Scripts/jquery-3.1.1.js", "~/Scripts/common.js", "~/Scripts/jquery.unobtrusive-ajax.min.js"));
























            BundleTable.EnableOptimizations = true;
            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备时，请使用 http://modernizr.com 上的生成工具来仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
