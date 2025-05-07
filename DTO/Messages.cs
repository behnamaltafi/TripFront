// مدل‌های پایه
using BusinessExceptionStructure;

public class Messages
{
    #region General


    public static MessageTemplate RecordNotFound { get; set; } = new MessageTemplate("رکورد مورد نظر یافت نشد", 1);


  
    #region Empty / NotFound
    public const string InputIsEmpty = "ورودی خالی میباشد";
    public const string InputParameterValueIsEmpty = "مقدار پارامتر ورودی خالی میباشد";
    public const string SomeInputsAreEmpty = "برخی از ورودی ها خالی میباشند";
    public const string InputIsNotValid = "ورودی معتبر نمیباشد";
    public const string IdIsEmpty = "شناسه خالی میباشد";
    public const string MobileNumberIsEmpty = "شماره موبایل خالی میباشد";
    public const string MobileNumberIsNotValid = "شماره موبایل معتبر نمیباشد";
    public const string UserMobileNumberIsNotValid = "شماره موبایل کاربر معتبر نمیباشد";
    public const string ObjectNotFound = "{0} یافت نشد";
    public const string ObjectIsEmpty = "{0} خالی است";
    public const string ObjectsAreEmpty = "{0} خالی میباشند";
    public const string FileNotFound = "فایل {0} یافت نشد";
    #endregion

    #region Exception
    public const string CallApiFailed = "فراخوانی وب سرویس با خطا روبرو شد";
    public const string CallSomeApiFailed = "فراخوانی وب سرویس {0} با خطا روبرو شد";
    public const string MessageBusPublishError = "ارسال به صف پیام با خطا روبرو شد";
    public const string SendSmsFailed = "ارسال پیام کوتاه با خطا روبرو شد";
    public const string OperationFailed = "عملیات با خطا روبرو شد";
    public const string OperationForbidden = "امکان اجرای عملیات وجود ندارد";
    public const string GatewayApiCallFailed = "فراخوانی وب سرویس Gateway با خطا روبرو شد";
    public const string GalaxyApiCallFailed = "فراخوانی وب سرویس کهکشان با خطا روبرو شد";

    #endregion

    #region Database
    public const string RecordHasReference = "رکورد، وابسته دارد، ابتدا وابسته ها میبایست حذف بشود";
    public const string ForeignKeyConstraint = "خطای عدم تطابق کلید خارجی در پایگاه داده رخ داده است";
    public const string UniqueIndex = "مقدار وارد شده تکراری می باشد";
    public const string DbUpdateConcurrencyException = "خطای همزمانی در پایگاه داده رخ داده است";
    #endregion

    #region Access
    public const string UserHasNoAccessToRecord = "کاربر به رکورد دسترسی ندارد";
    #endregion

    #region Repetetive
    public const string FileAlreadyExist = "فایل از قبل وجود دارد";
    public const string RecordIsRepetetive = "رکورد تکراری میباشد";
    public const string TitleIsRepetetive = "عنوان تکراری میباشد";
    public const string FieldIsRepetetive = "{0} تکراری میباشد";
    public const string InputFilesAreRepetetive = "برخی از فایل های ورودی با هم مشابه هستند";
    #endregion

    #region File
    public const string FileIsEmpty = "فایل '{0}' فاقد محتوا میباشد";
    public const string MaxFileNameSizeIs100 = "نام فایل '{0}' بیش از 100 کاراکتر میباشد";
    public const string NotAllowedFileContentType = "نوع فایل '{0}' مجاز نمیباشد";
    public const string InvalidFileSize = "حجم فایل {0} نمی بایست بیش از {1} {2} باشد ('{3}')";
    public const string FileContentDoesNotMatchItsExtension = "محتوا فایل '{0}' با پسوند فایل تطابق ندارد";
    #endregion

    #region Other
    public const string EntityForAttachmentNotFound = "رکورد مربوط به پیوست یافت نشد";
    public const string ConvertReportToPdfFailed = "تبدیل گزارش به PDF با خطا روبرو شد";
    #endregion

    #endregion

    #region Http Status Codes
    public static MessageTemplate HttpCode_OK { get; set; } = new MessageTemplate("عملیات با موفقیت انجام شد", 200);
    public static MessageTemplate HttpCode_BadRequest { get; set; } = new MessageTemplate("خطای کاربر رخ داده است", 400);
    public static MessageTemplate HttpCode_Unauthorized { get; set; } = new MessageTemplate("کاربر احراز هویت نشده است", 401);
    public static MessageTemplate HttpCode_Forbidden { get; set; } = new MessageTemplate("کاربر دسترسی ندارد", 403);
    public static MessageTemplate HttpCode_NotFound { get; set; } = new MessageTemplate("محتوای مورد نظر یافت نشد", 404);
    public static MessageTemplate HttpCode_InternalServerError { get; set; } = new MessageTemplate("خطای سرور رخ داده است", 500);
    #endregion
}