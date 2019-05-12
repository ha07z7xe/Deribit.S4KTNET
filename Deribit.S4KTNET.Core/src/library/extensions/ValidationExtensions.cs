//using FluentValidation.Results;
//using System;
//using System.Linq;

//namespace Deribit.S4KTNET.Core
//{
//    internal static class ValidationExtensions
//    {
//        public static void ThrowIfInvalid(this ValidationResult vr)
//        {
//            if (vr.IsValid)
//                return;
//            ValidationFailure vf = vr.Errors.First();
//            string message = $"{vf.ErrorCode} | {vf.ErrorMessage}";
//            throw new Exception(message);
//        }
//    }
//}