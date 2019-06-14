﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZZULIWeb
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ZZULIEntities : DbContext
    {
        public ZZULIEntities()
            : base("name=ZZULIEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Courses> Courses { get; set; }
        public virtual DbSet<CoursesAnswers> CoursesAnswers { get; set; }
        public virtual DbSet<CoursesComment> CoursesComment { get; set; }
        public virtual DbSet<CoursesNotes> CoursesNotes { get; set; }
        public virtual DbSet<CoursesQuestions> CoursesQuestions { get; set; }
        public virtual DbSet<Follow> Follow { get; set; }
        public virtual DbSet<Move> Move { get; set; }
        public virtual DbSet<Navigations> Navigations { get; set; }
        public virtual DbSet<NewAry> NewAry { get; set; }
        public virtual DbSet<Notes> Notes { get; set; }
        public virtual DbSet<NotesComments> NotesComments { get; set; }
        public virtual DbSet<ObjectTags> ObjectTags { get; set; }
        public virtual DbSet<OptionType> OptionType { get; set; }
        public virtual DbSet<Partition> Partition { get; set; }
        public virtual DbSet<Priority> Priority { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<ResourceComment> ResourceComment { get; set; }
        public virtual DbSet<ResourceInfo> ResourceInfo { get; set; }
        public virtual DbSet<ResourceType> ResourceType { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<UserAry> UserAry { get; set; }
        public virtual DbSet<UserCollections> UserCollections { get; set; }
        public virtual DbSet<UserFollows> UserFollows { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserLearnCoursesRecord> UserLearnCoursesRecord { get; set; }
        public virtual DbSet<UserLearnSectionRecord> UserLearnSectionRecord { get; set; }
        public virtual DbSet<UserLikes> UserLikes { get; set; }
        public virtual DbSet<UserNotLikes> UserNotLikes { get; set; }
        public virtual DbSet<UserPageViews> UserPageViews { get; set; }
        public virtual DbSet<UserSearchRecord> UserSearchRecord { get; set; }
        public virtual DbSet<View_All> View_All { get; set; }
        public virtual DbSet<View_learn> View_learn { get; set; }
    
        public virtual ObjectResult<Nullable<int>> PR_ALL(Nullable<int> user_ID, Nullable<int> nav_ID)
        {
            var user_IDParameter = user_ID.HasValue ?
                new ObjectParameter("User_ID", user_ID) :
                new ObjectParameter("User_ID", typeof(int));
    
            var nav_IDParameter = nav_ID.HasValue ?
                new ObjectParameter("Nav_ID", nav_ID) :
                new ObjectParameter("Nav_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("PR_ALL", user_IDParameter, nav_IDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> PR_AllID()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("PR_AllID");
        }
    
        public virtual ObjectResult<Nullable<int>> PR_CouID()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("PR_CouID");
        }
    
        public virtual ObjectResult<Nullable<int>> PR_Learn(Nullable<int> user_ID, Nullable<int> nav_ID)
        {
            var user_IDParameter = user_ID.HasValue ?
                new ObjectParameter("User_ID", user_ID) :
                new ObjectParameter("User_ID", typeof(int));
    
            var nav_IDParameter = nav_ID.HasValue ?
                new ObjectParameter("Nav_ID", nav_ID) :
                new ObjectParameter("Nav_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("PR_Learn", user_IDParameter, nav_IDParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> PR_learnID()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("PR_learnID");
        }
    
        public virtual int PR_NewAry(Nullable<int> user_ID, Nullable<int> nav_ID, Nullable<double> ary_Sco)
        {
            var user_IDParameter = user_ID.HasValue ?
                new ObjectParameter("User_ID", user_ID) :
                new ObjectParameter("User_ID", typeof(int));
    
            var nav_IDParameter = nav_ID.HasValue ?
                new ObjectParameter("Nav_ID", nav_ID) :
                new ObjectParameter("Nav_ID", typeof(int));
    
            var ary_ScoParameter = ary_Sco.HasValue ?
                new ObjectParameter("Ary_Sco", ary_Sco) :
                new ObjectParameter("Ary_Sco", typeof(double));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PR_NewAry", user_IDParameter, nav_IDParameter, ary_ScoParameter);
        }
    
        public virtual ObjectResult<PR_SelAll_Result> PR_SelAll(Nullable<int> user_ID, Nullable<int> nav_ID)
        {
            var user_IDParameter = user_ID.HasValue ?
                new ObjectParameter("User_ID", user_ID) :
                new ObjectParameter("User_ID", typeof(int));
    
            var nav_IDParameter = nav_ID.HasValue ?
                new ObjectParameter("Nav_ID", nav_ID) :
                new ObjectParameter("Nav_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PR_SelAll_Result>("PR_SelAll", user_IDParameter, nav_IDParameter);
        }
    
        public virtual ObjectResult<PR_SelectAll_Result> PR_SelectAll()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PR_SelectAll_Result>("PR_SelectAll");
        }
    
        public virtual int PR_UserAry(Nullable<int> user_ID, Nullable<int> nav_ID, Nullable<int> ary_Sco)
        {
            var user_IDParameter = user_ID.HasValue ?
                new ObjectParameter("User_ID", user_ID) :
                new ObjectParameter("User_ID", typeof(int));
    
            var nav_IDParameter = nav_ID.HasValue ?
                new ObjectParameter("Nav_ID", nav_ID) :
                new ObjectParameter("Nav_ID", typeof(int));
    
            var ary_ScoParameter = ary_Sco.HasValue ?
                new ObjectParameter("Ary_Sco", ary_Sco) :
                new ObjectParameter("Ary_Sco", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("PR_UserAry", user_IDParameter, nav_IDParameter, ary_ScoParameter);
        }
    
        public virtual ObjectResult<Nullable<int>> PR_UserID()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("PR_UserID");
        }
    
        public virtual int SP_Account_IsExist(string account, ObjectParameter isExist)
        {
            var accountParameter = account != null ?
                new ObjectParameter("Account", account) :
                new ObjectParameter("Account", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_Account_IsExist", accountParameter, isExist);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<SP_GetAllCourses_Result> SP_GetAllCourses()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetAllCourses_Result>("SP_GetAllCourses");
        }
    
        public virtual int SP_GetCatalogByCou_ID(Nullable<int> cou_ID)
        {
            var cou_IDParameter = cou_ID.HasValue ?
                new ObjectParameter("Cou_ID", cou_ID) :
                new ObjectParameter("Cou_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_GetCatalogByCou_ID", cou_IDParameter);
        }
    
        public virtual int SP_GetCourseCatalog_PageViewsINC(Nullable<int> cou_ID)
        {
            var cou_IDParameter = cou_ID.HasValue ?
                new ObjectParameter("Cou_ID", cou_ID) :
                new ObjectParameter("Cou_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_GetCourseCatalog_PageViewsINC", cou_IDParameter);
        }
    
        public virtual ObjectResult<SP_GetCourses_Result> SP_GetCourses()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetCourses_Result>("SP_GetCourses");
        }
    
        public virtual ObjectResult<SP_GetCoursesTopN_OrderByHot_Result> SP_GetCoursesTopN_OrderByHot(Nullable<int> num)
        {
            var numParameter = num.HasValue ?
                new ObjectParameter("Num", num) :
                new ObjectParameter("Num", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetCoursesTopN_OrderByHot_Result>("SP_GetCoursesTopN_OrderByHot", numParameter);
        }
    
        public virtual ObjectResult<SP_GetCoursesTopN_OrderByStudyNum_Result> SP_GetCoursesTopN_OrderByStudyNum(Nullable<int> num)
        {
            var numParameter = num.HasValue ?
                new ObjectParameter("Num", num) :
                new ObjectParameter("Num", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetCoursesTopN_OrderByStudyNum_Result>("SP_GetCoursesTopN_OrderByStudyNum", numParameter);
        }
    
        public virtual ObjectResult<SP_GetFuncGroup_Result> SP_GetFuncGroup()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_GetFuncGroup_Result>("SP_GetFuncGroup");
        }
    
        public virtual int SP_GetNavigation()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_GetNavigation");
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<SP_Login_IsSuccess_Result> SP_Login_IsSuccess(string account, string pwd, ObjectParameter isSuccess)
        {
            var accountParameter = account != null ?
                new ObjectParameter("Account", account) :
                new ObjectParameter("Account", typeof(string));
    
            var pwdParameter = pwd != null ?
                new ObjectParameter("Pwd", pwd) :
                new ObjectParameter("Pwd", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_Login_IsSuccess_Result>("SP_Login_IsSuccess", accountParameter, pwdParameter, isSuccess);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int SP_UpdateCourseHotByCou_ID(Nullable<int> cou_ID)
        {
            var cou_IDParameter = cou_ID.HasValue ?
                new ObjectParameter("Cou_ID", cou_ID) :
                new ObjectParameter("Cou_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateCourseHotByCou_ID", cou_IDParameter);
        }
    
        public virtual int SP_UpdateCourseNoteHot(Nullable<int> n_ID)
        {
            var n_IDParameter = n_ID.HasValue ?
                new ObjectParameter("N_ID", n_ID) :
                new ObjectParameter("N_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateCourseNoteHot", n_IDParameter);
        }
    
        public virtual int SP_UpdateCourseQuestionHot(Nullable<int> cQ_ID)
        {
            var cQ_IDParameter = cQ_ID.HasValue ?
                new ObjectParameter("CQ_ID", cQ_ID) :
                new ObjectParameter("CQ_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateCourseQuestionHot", cQ_IDParameter);
        }
    
        public virtual int SP_UpdateNoteHotByN_ID(Nullable<int> n_ID)
        {
            var n_IDParameter = n_ID.HasValue ?
                new ObjectParameter("N_ID", n_ID) :
                new ObjectParameter("N_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateNoteHotByN_ID", n_IDParameter);
        }
    
        public virtual int SP_UpdateQuestionHotByQ_ID(Nullable<int> q_ID)
        {
            var q_IDParameter = q_ID.HasValue ?
                new ObjectParameter("Q_ID", q_ID) :
                new ObjectParameter("Q_ID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("SP_UpdateQuestionHotByQ_ID", q_IDParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    }
}
