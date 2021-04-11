using System;
using System.Security.Cryptography;
using System.Text;

namespace Utility
{
    public static class Helper
    {
        public static string EncryptPass(this string passwordPlainText)
        {
            string plainText = passwordPlainText.Trim();
            string cipherText = "";                 // encrypted text
            string passPhrase = "Pas5pr@se";        // can be any string
            string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
            EncryptionClass thisKey = new EncryptionClass(passPhrase, initVector);
            cipherText = thisKey.Encrypt(plainText);
            return cipherText;

        }
        public static string DecryptPass(this string cipherText)
        {
            var plainText = "";
            // var cipherText = CipherText;                 // encrypted text
            var passPhrase = "Pas5pr@se";        // can be any string
            var initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes

            EncryptionClass thisKey = new EncryptionClass(passPhrase, initVector);
            plainText = thisKey.Decrypt(cipherText);

            return plainText;

        }
        public static string CalculateMd5Hash(this string passwordPlainText)
        {
            // step 1, calculate MD5 hash from input
            var md5 = System.Security.Cryptography.MD5.Create();
            var inputBytes = System.Text.Encoding.ASCII.GetBytes(passwordPlainText);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(i.ToString("X2"));
            }
            return sb.ToString();
        }

        public static string ToStudentStateName(this int stateId)
        {
            switch (stateId)
            {
                case 1:
                    return "عادی";
                case 2:
                    return "میهمان";
                case 3:
                    return "انصراف_تغییر رشته";
                case 4:
                    return "انتقال به";
                case 5:
                    return "انصراف با اطلاع";
                case 6:
                    return "انصراف ماده 51";
                case 7:
                    return "فارغ التحصیل";
                case 8:
                    return "اخراج آموزشی";
                case 9:
                    return "اخراج انضباطی از واحد";
                case 10:
                    return "اخراج از واحد های تهران";
                case 11:
                    return "اخراج از کل واحدها";
                case 12:
                    return "محروم";
                case 13:
                    return "فوت";
                case 14:
                    return "شهید";
                case 15:
                    return "ميهمان از - سازمان";
                case 16:
                    return "عدم مراجعه";
                case 17:
                    return "در شرف فارغ التحصیل";
                case 18:
                    return "تسویه حساب_مدرک معادل";
                default:
                    return "نامشخص";
            }
        }
        public static string ToProfessorStateName(this int stateId)
        {

            switch (stateId)
            {
                case 1:
                    return "مشغول به کار";
                case 2:
                    return "بـــازنـشسته";
                case 3:
                    return "اخـــــــراج";
                case 4:
                    return "قطع همکــاري";
                case 5:
                    return "فقط پایان نامه";

                default:
                    return "نامشخص";
            }
        }


        public static string ToEmployeeStateName(this int stateId)
        {

            switch (stateId)
            {
                case 1:
                    return "مشغول به کار";
                case 2:
                    return "بـــازنـشسته";
                case 3:
                    return "اخـــــــراج";
                case 4:
                    return "قطع همکــاري";

                default:
                    return "نامشخص";
            }
        }
        public static string ToStudentDegreeName(this int degreeId)
        {
            switch (degreeId)
            {
                case 1:
                    return "کارشناسی";
                case 2:
                    return "کاردانی";
                case 3:
                    return "کارشناسی ناپیوسته";
                case 4:
                    return "ارشد پیوسته";
                case 5:
                    return "ارشد ناپیوسته";
                case 6:
                    return "دکتری";
                case 7:
                    return "دکتری";
                case 8:
                    return "کاردانی پیوسته";
                default:
                    return string.Empty;
            }
        }

        public static int ToInt(this string str)
        {
            if (str == null) return 0;
            return Int32.Parse(str);
        }
        public static bool IsActiveStudent(this int stateId)
        {
            return stateId == 1 || stateId == 2 || stateId == 15 || stateId == 17;
        }
        public static bool IsActiveProfessor(this int stateId)
        {
            return stateId == 1 || stateId == 5;
        }

        public static bool IsActiveEmployee(this int stateId)
        {
            return stateId == 1;
        }



        public enum ApiOutputError
        {
            NoError = 0,
            InCorrectPassword = 1,
            NotExists = 2,
            InCorrectRandomNumber = 3,
            BadRequest = 4,
            InCorrectUserNameOrPassword = 5,
        }

        public enum ObjectType
        {
            Student = 1,
            Professor = 2,
            Employee = 3
        }

        public enum ProfileType
        {
            Student = 0,
            Professor = 1,
            Employee =2
        }
        public static string GetCurrentTerm(BaseServiceConfig config)
        {
            var currentTerm = ClientHelper.GetScalarValue<string>(StaticValue.CurrentTermRelativeAddress, config);
            return currentTerm;
        }
    }
}