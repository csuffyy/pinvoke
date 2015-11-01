﻿// Copyright (c) to owners found in https://github.com/AArnott/pinvoke/blob/master/COPYRIGHT.md. All rights reserved.
// Licensed under the MIT license. See LICENSE.txt file in the project root for full license information.

namespace PInvoke
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Exported functions from the BCrypt.dll Windows library.
    /// </summary>
    public static partial class BCrypt
    {
        /// <summary>
        /// Types of data stored in <see cref="BCryptBuffer"/>.
        /// </summary>
        public enum BufferType
        {
            KDF_HASH_ALGORITHM = 0,
            KDF_SECRET_PREPEND = 1,
            KDF_SECRET_APPEND = 2,
            KDF_HMAC_KEY = 3,
            KDF_TLS_PRF_LABEL = 4,
            KDF_TLS_PRF_SEED = 5,
            KDF_SECRET_HANDLE = 6,
            KDF_TLS_PRF_PROTOCOL = 7,
            KDF_ALGORITHMID = 8,
            KDF_PARTYUINFO = 9,
            KDF_PARTYVINFO = 10,
            KDF_SUPPPUBINFO = 11,
            KDF_SUPPPRIVINFO = 12,
            KDF_LABEL = 13,
            KDF_CONTEXT = 14,
            KDF_SALT = 15,
            KDF_ITERATION_COUNT = 16,
        }

        [Flags]
        public enum BCryptSetPropertyFlags
        {
            None = 0x0,
        }

        [Flags]
        public enum BCryptGetPropertyFlags
        {
            None = 0x0,
        }

        [Flags]
        public enum BCryptCloseAlgorithmProviderFlags
        {
            None = 0x0,
        }

        [Flags]
        public enum BCryptSecretAgreementFlags
        {
            None = 0x0,
        }

        [Flags]
        public enum BCryptGenerateKeyPairFlags
        {
            None = 0x0,
        }

        [Flags]
        public enum BCryptFinalizeKeyPairFlags
        {
            None = 0x0,
        }

        [Flags]
        public enum BCryptImportKeyPairFlags
        {
            None = 0x0,

            /// <summary>
            /// Do not validate the public portion of the key pair.
            /// </summary>
            BCRYPT_NO_KEY_VALIDATION = 0x00000008,
        }

        [Flags]
        public enum BCryptImportKeyFlags
        {
            None = 0x0,
        }

        [Flags]
        public enum BCryptExportKeyFlags
        {
            None = 0x0,
        }

        [Flags]
        public enum BCryptGenRandomFlags
        {
            None = 0x0,

            /// <summary>
            /// This function will use the number in the pbBuffer buffer as additional entropy for the random number. If this flag is not specified, this function will use a random number for the entropy.
            /// Windows 8 and later:  This flag is ignored in Windows 8 and later.
            /// </summary>
            UseEntropyInBuffer = 0x1,

            /// <summary>
            /// Use the system-preferred random number generator algorithm. The hAlgorithm parameter must be NULL.
            /// <see cref="UseSystemPreferredRNG"/> is only supported at PASSIVE_LEVEL IRQL. For more information, see Remarks.
            /// Windows Vista:  This flag is not supported.
            /// </summary>
            UseSystemPreferredRNG = 0x2,
        }

        /// <summary>
        /// Flags that can be passed to <see cref="BCryptOpenAlgorithmProvider(string, string, BCryptOpenAlgorithmProviderFlags)"/>
        /// </summary>
        [Flags]
        public enum BCryptOpenAlgorithmProviderFlags
        {
            /// <summary>
            /// No flags.
            /// </summary>
            None = 0x0,

            /// <summary>
            /// The provider will perform the Hash-Based Message Authentication Code (HMAC)
            /// algorithm with the specified hash algorithm. This flag is only used by hash
            /// algorithm providers.
            /// </summary>
            AlgorithmHandleHmac,

            /// <summary>
            /// Creates a reusable hashing object. The object can be used for a new hashing
            /// operation immediately after calling BCryptFinishHash. For more information,
            /// see Creating a Hash with CNG.
            /// </summary>
            HashReusable,
        }

        [Flags]
        public enum BCryptDeriveKeyFlags
        {
            /// <summary>
            /// No flags.
            /// </summary>
            None = 0x0,

            /// <summary>
            /// Causes the secret agreement to serve also
            /// as the HMAC key.  If this flag is used, the KDF_HMAC_KEY parameter should
            /// NOT be specified.
            /// </summary>
            KDF_USE_SECRET_AS_HMAC_KEY_FLAG = 0x1,
        }

        /// <summary>
        /// Flags that can be passed to the <see cref="BCryptEncrypt"/> method.
        /// </summary>
        [Flags]
        public enum BCryptEncryptFlags
        {
            None = 0x0,

            /// <summary>
            /// Symmetric algorithms: Allows the encryption algorithm to pad the data to the next block size. If this flag is not specified, the size of the plaintext specified in the cbInput parameter must be a multiple of the algorithm's block size.
            /// The block size can be obtained by calling the <see cref="BCryptGetProperty(SafeHandle, string, BCryptGetPropertyFlags)"/> function to get the BCRYPT_BLOCK_LENGTH property for the key. This will provide the size of a block for the algorithm.
            /// This flag must not be used with the authenticated encryption modes (AES-CCM and AES-GCM).
            /// </summary>
            BCRYPT_BLOCK_PADDING = 1,

            /// <summary>
            /// Asymmetric algorithms: Do not use any padding. The pPaddingInfo parameter is not used. The size of the plaintext specified in the cbInput parameter must be a multiple of the algorithm's block size.
            /// </summary>
            BCRYPT_PAD_NONE = 0x1,

            /// <summary>
            /// Asymmetric algorithms: The data will be padded with a random number to round out the block size. The pPaddingInfo parameter is not used.
            /// </summary>
            BCRYPT_PAD_PKCS1 = 0x2,

            /// <summary>
            /// Asymmetric algorithms: Use the Optimal Asymmetric Encryption Padding (OAEP) scheme. The pPaddingInfo parameter is a pointer to a BCRYPT_OAEP_PADDING_INFO structure.
            /// </summary>
            BCRYPT_PAD_OAEP = 0x4,
        }

        /// <summary>
        /// Flags that may be passed to the <see cref="BCryptGenerateSymmetricKey(SafeAlgorithmHandle, byte[], byte[], BCryptGenerateSymmetricKeyFlags)"/> method.
        /// </summary>
        [Flags]
        public enum BCryptGenerateSymmetricKeyFlags
        {
            None = 0x0,
        }

        /// <summary>
        /// Loads and initializes a CNG provider.
        /// </summary>
        /// <param name="phAlgorithm">
        /// A pointer to a BCRYPT_ALG_HANDLE variable that receives the CNG provider handle.
        /// When you have finished using this handle, release it by passing it to the
        /// BCryptCloseAlgorithmProvider function.
        /// </param>
        /// <param name="pszAlgId">
        /// A pointer to a null-terminated Unicode string that identifies the requested
        /// cryptographic algorithm. This can be one of the standard
        /// CNG Algorithm Identifiers defined in <see cref="AlgorithmIdentifiers"/>
        /// or the identifier for another registered algorithm.
        /// </param>
        /// <param name="pszImplementation">
        /// <para>
        /// A pointer to a null-terminated Unicode string that identifies the specific provider
        /// to load. This is the registered alias of the cryptographic primitive provider.
        /// This parameter is optional and can be NULL if it is not needed. If this parameter
        /// is NULL, the default provider for the specified algorithm will be loaded.
        /// </para>
        /// <para>
        /// Note If the <paramref name="pszImplementation"/> parameter value is NULL, CNG attempts to open each
        /// registered provider, in order of priority, for the algorithm specified by the
        /// <paramref name="pszAlgId"/> parameter and returns the handle of the first provider that is successfully
        /// opened.For the lifetime of the handle, any BCrypt*** cryptographic APIs will use the
        /// provider that was successfully opened.
        /// </para>
        /// </param>
        /// <param name="dwFlags">Options for the function.</param>
        /// <returns>
        /// Returns a status code that indicates the success or failure of the function.
        /// </returns>
        [DllImport(nameof(BCrypt), SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern NTStatus BCryptOpenAlgorithmProvider(
            out SafeAlgorithmHandle phAlgorithm,
            string pszAlgId,
            string pszImplementation,
            BCryptOpenAlgorithmProviderFlags dwFlags);

        /// <summary>
        /// Encrypts a block of data.
        /// </summary>
        /// <param name="hKey">
        /// The handle of the key to use to encrypt the data. This handle is obtained from one of the key creation functions, such as <see cref="BCryptGenerateSymmetricKey(SafeAlgorithmHandle, byte[], byte[], BCryptGenerateSymmetricKeyFlags)"/>, <see cref="BCryptGenerateKeyPair(SafeAlgorithmHandle, int)"/>, or <see cref="BCryptImportKey"/>.
        /// </param>
        /// <param name="pbInput">
        /// The address of a buffer that contains the plaintext to be encrypted. The cbInput parameter contains the size of the plaintext to encrypt.
        /// </param>
        /// <param name="cbInput">
        /// The number of bytes in the pbInput buffer to encrypt.
        /// </param>
        /// <param name="pPaddingInfo">
        /// A pointer to a structure that contains padding information. This parameter is only used with asymmetric keys and authenticated encryption modes. If an authenticated encryption mode is used, this parameter must point to a BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO structure. If asymmetric keys are used, the type of structure this parameter points to is determined by the value of the dwFlags parameter. Otherwise, the parameter must be set to NULL.
        /// </param>
        /// <param name="pbIV">
        /// The address of a buffer that contains the initialization vector (IV) to use during encryption. The cbIV parameter contains the size of this buffer. This function will modify the contents of this buffer. If you need to reuse the IV later, make sure you make a copy of this buffer before calling this function.
        /// This parameter is optional and can be NULL if no IV is used.
        /// The required size of the IV can be obtained by calling the <see cref="BCryptGetProperty(SafeHandle, string, BCryptGetPropertyFlags)"/> function to get the BCRYPT_BLOCK_LENGTH property.This will provide the size of a block for the algorithm, which is also the size of the IV.
        /// </param>
        /// <param name="cbIV">The size, in bytes, of the pbIV buffer.</param>
        /// <param name="pbOutput">
        /// The address of the buffer that receives the ciphertext produced by this function. The <paramref name="cbOutput"/> parameter contains the size of this buffer. For more information, see Remarks.
        /// If this parameter is NULL, the <see cref="BCryptEncrypt"/> function calculates the size needed for the ciphertext of the data passed in the <paramref name="pbInput"/> parameter. In this case, the location pointed to by the <paramref name="pcbResult"/> parameter contains this size, and the function returns <see cref="NTStatus.STATUS_SUCCESS"/>.The <paramref name="pPaddingInfo"/> parameter is not modified.
        /// If the values of both the <paramref name="pbOutput"/> and <paramref name="pbInput"/> parameters are NULL, an error is returned unless an authenticated encryption algorithm is in use.In the latter case, the call is treated as an authenticated encryption call with zero length data, and the authentication tag is returned in the <paramref name="pPaddingInfo"/> parameter.
        /// </param>
        /// <param name="cbOutput">
        /// The size, in bytes, of the <paramref name="pbOutput"/> buffer. This parameter is ignored if the <paramref name="pbOutput"/> parameter is NULL.
        /// </param>
        /// <param name="pcbResult">
        /// A pointer to a ULONG variable that receives the number of bytes copied to the <paramref name="pbOutput"/> buffer. If <paramref name="pbOutput"/> is NULL, this receives the size, in bytes, required for the ciphertext.
        /// </param>
        /// <param name="dwFlags">
        /// A set of flags that modify the behavior of this function. The allowed set of flags depends on the type of key specified by the hKey parameter.
        /// </param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        /// <remarks>
        /// The <paramref name="pbInput"/> and <paramref name="pbOutput"/> parameters can point to the same buffer. In this case, this function will perform the encryption in place. It is possible that the encrypted data size will be larger than the unencrypted data size, so the buffer must be large enough to hold the encrypted data.
        /// </remarks>
        [DllImport(nameof(BCrypt), SetLastError = true)]
        public static extern NTStatus BCryptEncrypt(
            SafeKeyHandle hKey,
            byte[] pbInput,
            int cbInput,
            IntPtr pPaddingInfo,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[] pbIV,
            int cbIV,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 8)] byte[] pbOutput,
            int cbOutput,
            out int pcbResult,
            BCryptEncryptFlags dwFlags);

        /// <summary>
        /// Decrypts a block of data.
        /// </summary>
        /// <param name="hKey">
        /// The handle of the key to use to decrypt the data. This handle is obtained from one of the key creation functions, such as <see cref="BCryptGenerateSymmetricKey(SafeAlgorithmHandle, byte[], byte[], BCryptGenerateSymmetricKeyFlags)"/>, <see cref="BCryptGenerateKeyPair(SafeAlgorithmHandle, int)"/>, or <see cref="BCryptImportKey"/>.
        /// </param>
        /// <param name="pbInput">
        /// The address of a buffer that contains the ciphertext to be decrypted. The <paramref name="cbInput"/> parameter contains the size of the ciphertext to decrypt. For more information, see Remarks.
        /// </param>
        /// <param name="cbInput">
        /// The number of bytes in the <paramref name="pbInput"/> buffer to decrypt.
        /// </param>
        /// <param name="pPaddingInfo">
        /// A pointer to a structure that contains padding information. This parameter is only used with asymmetric keys and authenticated encryption modes. If an authenticated encryption mode is used, this parameter must point to a BCRYPT_AUTHENTICATED_CIPHER_MODE_INFO structure. If asymmetric keys are used, the type of structure this parameter points to is determined by the value of the <paramref name="dwFlags"/> parameter. Otherwise, the parameter must be set to NULL.
        /// </param>
        /// <param name="pbIV">
        /// The address of a buffer that contains the initialization vector (IV) to use during decryption. The <paramref name="cbIV"/> parameter contains the size of this buffer. This function will modify the contents of this buffer. If you need to reuse the IV later, make sure you make a copy of this buffer before calling this function.
        /// This parameter is optional and can be NULL if no IV is used.
        /// The required size of the IV can be obtained by calling the <see cref="BCryptGetProperty(SafeHandle, string, BCryptGetPropertyFlags)"/> function to get the <see cref="PropertyNames.BlockLength"/> property. This will provide the size of a block for the algorithm, which is also the size of the IV.
        /// </param>
        /// <param name="cbIV">
        /// The size, in bytes, of the <paramref name="pbIV"/> buffer.
        /// </param>
        /// <param name="pbOutput">
        /// The address of a buffer to receive the plaintext produced by this function. The cbOutput parameter contains the size of this buffer. For more information, see Remarks.
        /// If this parameter is NULL, the <see cref="BCryptDecrypt"/> function calculates the size required for the plaintext of the encrypted data passed in the <paramref name="pbInput"/> parameter.In this case, the location pointed to by the <paramref name="pcbResult"/> parameter contains this size, and the function returns <see cref="NTStatus.STATUS_SUCCESS"/>.
        /// If the values of both the <paramref name="pbOutput"/> and <paramref name="pbInput" /> parameters are NULL, an error is returned unless an authenticated encryption algorithm is in use.In the latter case, the call is treated as an authenticated encryption call with zero length data, and the authentication tag, passed in the <paramref name="pPaddingInfo"/> parameter, is verified.
        /// </param>
        /// <param name="cbOutput">
        /// The size, in bytes, of the <paramref name="pbOutput"/> buffer. This parameter is ignored if the <paramref name="pbOutput"/> parameter is NULL.
        /// </param>
        /// <param name="pcbResult">
        /// A pointer to a ULONG variable to receive the number of bytes copied to the <paramref name="pbOutput"/> buffer. If <paramref name="pbOutput"/> is NULL, this receives the size, in bytes, required for the plaintext.
        /// </param>
        /// <param name="dwFlags">
        /// A set of flags that modify the behavior of this function. The allowed set of flags depends on the type of key specified by the <paramref name="hKey"/> parameter.
        /// </param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        [DllImport(nameof(BCrypt), SetLastError = true)]
        public static extern NTStatus BCryptDecrypt(
            SafeKeyHandle hKey,
            byte[] pbInput,
            int cbInput,
            IntPtr pPaddingInfo,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[] pbIV,
            int cbIV,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 8)] byte[] pbOutput,
            int cbOutput,
            out int pcbResult,
            BCryptEncryptFlags dwFlags);

        /// <summary>
        /// Creates an empty public/private key pair.
        /// </summary>
        /// <param name="hAlgorithm">The handle to the algorithm previously opened by <see cref="BCryptOpenAlgorithmProvider(string, string, BCryptOpenAlgorithmProviderFlags)"/></param>
        /// <param name="phKey">Receives a handle to the generated key pair.</param>
        /// <param name="dwLength">The length of the key, in bits.</param>
        /// <param name="dwFlags">A set of flags that modify the behavior of this function. No flags are currently defined, so this parameter should be zero.</param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        /// <remarks>
        /// After you create a key by using this function, you can use the BCryptSetProperty
        /// function to set its properties; however, the key cannot be used until the
        /// BCryptFinalizeKeyPair function is called.
        /// </remarks>
        [DllImport(nameof(BCrypt), SetLastError = true, ExactSpelling = true)]
        public static extern NTStatus BCryptGenerateKeyPair(
            SafeAlgorithmHandle hAlgorithm,
            out SafeKeyHandle phKey,
            int dwLength,
            BCryptGenerateKeyPairFlags dwFlags = BCryptGenerateKeyPairFlags.None);

        /// <summary>
        /// Creates a key object for use with a symmetrical key encryption algorithm from a supplied key.
        /// </summary>
        /// <param name="hAlgorithm">
        /// The handle of an algorithm provider created with the <see cref="BCryptOpenAlgorithmProvider(string, string, BCryptOpenAlgorithmProviderFlags)"/> function. The algorithm specified when the provider was created must support symmetric key encryption.
        /// </param>
        /// <param name="phKey">
        /// Receives the <see cref="SafeKeyHandle"/> of the generated key.
        /// </param>
        /// <param name="pbKeyObject">
        /// A pointer to a buffer that receives the key object. The <paramref name="cbKeyObject"/> parameter contains the size of this buffer. The required size of this buffer can be obtained by calling the <see cref="BCryptGetProperty(SafeHandle, string, BCryptGetPropertyFlags)"/> function to get the BCRYPT_OBJECT_LENGTH property. This will provide the size of the key object for the specified algorithm.
        /// This memory can only be freed after the <paramref name="phKey"/> key handle is destroyed.
        /// If the value of this parameter is NULL and the value of the <paramref name="cbKeyObject"/> parameter is zero, the memory for the key object is allocated and freed by this function.
        /// </param>
        /// <param name="cbKeyObject">
        /// The size, in bytes, of the <paramref name="pbKeyObject"/> buffer.
        /// If the value of this parameter is zero and the value of the <paramref name="pbKeyObject"/> parameter is NULL, the memory for the key object is allocated and freed by this function.
        /// </param>
        /// <param name="pbSecret">
        /// Pointer to a buffer that contains the key from which to create the key object. The <paramref name="cbSecret"/> parameter contains the size of this buffer. This is normally a hash of a password or some other reproducible data. If the data passed in exceeds the target key size, the data will be truncated and the excess will be ignored.
        /// Note: We strongly recommended that applications pass in the exact number of bytes required by the target key.
        /// </param>
        /// <param name="cbSecret">
        /// The size, in bytes, of the <paramref name="pbSecret"/> buffer.
        /// </param>
        /// <param name="flags">A set of flags that modify the behavior of this function. No flags are currently defined, so this parameter should be zero.</param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        [DllImport(nameof(BCrypt), SetLastError = true)]
        public static extern NTStatus BCryptGenerateSymmetricKey(
            SafeAlgorithmHandle hAlgorithm,
            out SafeKeyHandle phKey,
            byte[] pbKeyObject,
            int cbKeyObject,
            byte[] pbSecret,
            int cbSecret,
            BCryptGenerateSymmetricKeyFlags flags = BCryptGenerateSymmetricKeyFlags.None);

        /// <summary>
        /// Completes a public/private key pair.
        /// </summary>
        /// <param name="hKey">The handle of the key to complete. This handle is obtained by calling the BCryptGenerateKeyPair function.</param>
        /// <param name="dwFlags">A set of flags that modify the behavior of this function. No flags are currently defined, so this parameter should be zero.</param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        /// <remarks>
        /// The key cannot be used until this function has been called.
        /// After this function has been called, the BCryptSetProperty function
        /// can no longer be used for this key.
        /// </remarks>
        [DllImport(nameof(BCrypt), SetLastError = true, ExactSpelling = true)]
        public static extern NTStatus BCryptFinalizeKeyPair(
            SafeKeyHandle hKey,
            BCryptFinalizeKeyPairFlags dwFlags = BCryptFinalizeKeyPairFlags.None);

        /// <summary>
        /// Destroys a key.
        /// </summary>
        /// <param name="hKey">The handle of the key to destroy.</param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        [DllImport(nameof(BCrypt), SetLastError = true, ExactSpelling = true)]
        public static extern NTStatus BCryptDestroyKey(
            IntPtr hKey);

        /// <summary>
        /// Destroys a secret agreement handle that was created by using the BCryptSecretAgreement function.
        /// </summary>
        /// <param name="hSecret">The handle of the secret to destroy.</param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        [DllImport(nameof(BCrypt), SetLastError = true, ExactSpelling = true)]
        public static extern NTStatus BCryptDestroySecret(
            IntPtr hSecret);

        /// <summary>
        /// Imports a symmetric key from a key BLOB. The BCryptImportKeyPair function is used to import a public/private key pair.
        /// </summary>
        /// <param name="hAlgorithm">
        /// The handle of the algorithm provider to import the key. This handle is obtained by calling the <see cref="BCryptOpenAlgorithmProvider(string, string, BCryptOpenAlgorithmProviderFlags)"/> function.
        /// </param>
        /// <param name="hImportKey">
        /// The handle of the key encryption key needed to unwrap the key BLOB in the pbInput parameter.
        /// Note The handle must be supplied by the same provider that supplied the key that is being imported.
        /// </param>
        /// <param name="pszBlobType">
        /// An identifier that specifies the type of BLOB that is contained in the pbInput buffer.
        /// This can be one of the values defined in <see cref="SymmetricKeyBlobTypes"/>.
        /// </param>
        /// <param name="phKey">
        /// A pointer to a BCRYPT_KEY_HANDLE that receives the handle of the imported key. This handle is used in subsequent functions that require a key, such as BCryptEncrypt. This handle must be released when it is no longer needed by passing it to the <see cref="BCryptDestroyKey"/> function.
        /// </param>
        /// <param name="pbKeyObject">
        /// A pointer to a buffer that receives the imported key object.
        /// The <paramref name="cbKeyObject"/> parameter contains the size of this buffer.
        /// The required size of this buffer can be obtained by calling the <see cref="BCryptGetProperty(SafeHandle, string, BCryptGetPropertyFlags)"/>
        /// function to get the BCRYPT_OBJECT_LENGTH property. This will provide the size of the
        /// key object for the specified algorithm.
        /// This memory can only be freed after the phKey key handle is destroyed.
        /// </param>
        /// <param name="cbKeyObject">The size, in bytes, of the <paramref name="pbKeyObject"/> buffer.</param>
        /// <param name="pbInput">
        /// The address of a buffer that contains the key BLOB to import.
        /// The <paramref name="cbInput"/> parameter contains the size of this buffer.
        /// The <paramref name="pszBlobType"/> parameter specifies the type of key BLOB this buffer contains.
        /// </param>
        /// <param name="cbInput">
        /// The size, in bytes, of the <paramref name="pbInput" /> buffer.
        /// </param>
        /// <param name="dwFlags">A set of flags that modify the behavior of this function.</param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        [DllImport(nameof(BCrypt), SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern NTStatus BCryptImportKey(
            SafeAlgorithmHandle hAlgorithm,
            SafeKeyHandle hImportKey,
            [MarshalAs(UnmanagedType.LPWStr)] string pszBlobType,
            out SafeKeyHandle phKey,
            byte[] pbKeyObject,
            int cbKeyObject,
            byte[] pbInput,
            int cbInput,
            BCryptImportKeyFlags dwFlags = BCryptImportKeyFlags.None);

        /// <summary>
        /// Imports a public/private key pair from a key BLOB.
        /// </summary>
        /// <param name="hAlgorithm">The handle of the algorithm provider to import the key. This handle is obtained by calling the BCryptOpenAlgorithmProvider function.</param>
        /// <param name="hImportKey">This parameter is not currently used and should be NULL.</param>
        /// <param name="pszBlobType">an identifier that specifies the type of BLOB that is contained in the <paramref name="pbInput"/> buffer.</param>
        /// <param name="phKey">A pointer to a BCRYPT_KEY_HANDLE that receives the handle of the imported key. This handle is used in subsequent functions that require a key, such as BCryptSignHash. This handle must be released when it is no longer needed by passing it to the <see cref="BCryptDestroyKey"/> function.</param>
        /// <param name="pbInput">The address of a buffer that contains the key BLOB to import. The cbInput parameter contains the size of this buffer. The <paramref name="pszBlobType"/> parameter specifies the type of key BLOB this buffer contains.</param>
        /// <param name="cbInput">The size, in bytes, of the <paramref name="pbInput"/> buffer.</param>
        /// <param name="dwFlags">A set of flags that modify the behavior of this function. This can be zero or the following value: BCRYPT_NO_KEY_VALIDATION</param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        [DllImport(nameof(BCrypt), SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern NTStatus BCryptImportKeyPair(
            SafeAlgorithmHandle hAlgorithm,
            SafeKeyHandle hImportKey,
            [MarshalAs(UnmanagedType.LPWStr)] string pszBlobType,
            out SafeKeyHandle phKey,
            byte[] pbInput,
            int cbInput,
            BCryptImportKeyPairFlags dwFlags);

        /// <summary>
        /// Exports a key to a memory BLOB that can be persisted for later use.
        /// </summary>
        /// <param name="hKey">The handle of the key to export.</param>
        /// <param name="hExportKey">
        /// The handle of the key with which to wrap the exported key. Use this parameter when exporting BLOBs of type BCRYPT_AES_WRAP_KEY_BLOB; otherwise, set it to NULL.
        /// Note: The <paramref name="hExportKey"/> handle must be supplied by the same provider that supplied the hKey handle, and hExportKey must be a handle to a symmetric key that can be used in the Advanced Encryption Standard(AES) key wrap algorithm.When the hKey handle is from the Microsoft provider, hExportKey must be an AES key handle.
        /// </param>
        /// <param name="pszBlobType">
        /// An identifier that specifies the type of BLOB to export. This can be one of the values
        /// defined in the <see cref="AsymmetricKeyBlobTypes"/> or <see cref="SymmetricKeyBlobTypes"/> classes.
        /// </param>
        /// <param name="pbOutput">
        /// The address of a buffer that receives the key BLOB.
        /// The <paramref name="cbOutput"/> parameter contains the size of this buffer.
        /// If this parameter is NULL, this function will place the required size, in bytes, in the ULONG pointed to by the <paramref name="pcbResult"/> parameter.
        /// </param>
        /// <param name="cbOutput">
        /// Contains the size, in bytes, of the <paramref name="pbOutput"/> buffer.
        /// </param>
        /// <param name="pcbResult">
        /// A pointer to a ULONG that receives the number of bytes that were copied to the <paramref name="pbOutput"/> buffer.
        /// If the pbOutput parameter is NULL, this function will place the required size, in bytes,
        /// in the ULONG pointed to by this parameter.
        /// </param>
        /// <param name="dwFlags">A set of flags that modify the behavior of this function. </param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        [DllImport(nameof(BCrypt), SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern NTStatus BCryptExportKey(
            SafeKeyHandle hKey,
            SafeKeyHandle hExportKey,
            [MarshalAs(UnmanagedType.LPWStr)] string pszBlobType,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[] pbOutput,
            int cbOutput,
            out int pcbResult,
            BCryptExportKeyFlags dwFlags = BCryptExportKeyFlags.None);

        /// <summary>
        /// Creates a secret agreement value from a private and a public key.
        /// </summary>
        /// <param name="privateKey">
        /// The handle of the private key to use to create the secret agreement value.
        /// This key and the hPubKey key must come from the same CNG cryptographic algorithm provider.
        /// </param>
        /// <param name="publicKey">
        /// The handle of the public key to use to create the secret agreement value.
        /// This key and the hPrivKey key must come from the same CNG cryptographic algorithm provider.
        /// </param>
        /// <param name="secret">
        /// A pointer to a BCRYPT_SECRET_HANDLE that receives a handle that represents the
        /// secret agreement value. This handle must be released by passing it to the
        /// BCryptDestroySecret function when it is no longer needed.
        /// </param>
        /// <param name="flags">A set of flags that modify the behavior of this function. No flags are defined for this function.</param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        [DllImport(nameof(BCrypt), SetLastError = true, ExactSpelling = true)]
        public static extern NTStatus BCryptSecretAgreement(
            SafeKeyHandle privateKey,
            SafeKeyHandle publicKey,
            out SafeSecretHandle secret,
            BCryptSecretAgreementFlags flags = BCryptSecretAgreementFlags.None);

        /// <summary>
        /// Derives a key from a secret agreement value.
        /// </summary>
        /// <param name="sharedSecret">
        /// The secret agreement handle to create the key from.
        /// This handle is obtained from the BCryptSecretAgreement function.
        /// </param>
        /// <param name="keyDerivationFunction">
        /// The key derivation function.
        /// May come from the constants defined on the <see cref="KeyDerivationFunctions"/> class.
        /// </param>
        /// <param name="kdfParameters">
        /// The address of a BCryptBufferDesc structure that contains the KDF parameters.
        /// This parameter is optional and can be NULL if it is not needed.
        /// </param>
        /// <param name="derivedKey">
        /// The address of a buffer that receives the key. The cbDerivedKey parameter contains
        /// the size of this buffer. If this parameter is NULL, this function will place the
        /// required size, in bytes, in the ULONG pointed to by the pcbResult parameter.
        /// </param>
        /// <param name="derivedKeySize">
        /// The size, in bytes, of the pbDerivedKey buffer.
        /// </param>
        /// <param name="resultSize">
        /// A pointer to a ULONG that receives the number of bytes that were copied to the
        /// pbDerivedKey buffer. If the pbDerivedKey parameter is NULL, this function will
        /// place the required size, in bytes, in the ULONG pointed to by this parameter.
        /// </param>
        /// <param name="flags">
        /// A set of flags that modify the behavior of this function.
        /// This can be zero or the following value.
        /// </param>
        /// <returns>
        /// Returns a status code that indicates the success or failure of the function.
        /// </returns>
        [DllImport(nameof(BCrypt), SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern NTStatus BCryptDeriveKey(
            SafeSecretHandle sharedSecret,
            string keyDerivationFunction,
            [In] ref BCryptBufferDesc kdfParameters,
            [Out, MarshalAs(UnmanagedType.LPArray)] byte[] derivedKey,
            int derivedKeySize,
            [Out] out int resultSize,
            BCryptDeriveKeyFlags flags);

        /// <summary>
        /// Sets the value of a named property for a CNG object.
        /// </summary>
        /// <param name="hObject">A handle that represents the CNG object to set the property value for.</param>
        /// <param name="property">
        /// A pointer to a null-terminated Unicode string that contains the name of the property to set. This can be one of the predefined <see cref="PropertyNames"/> or a custom property identifier.
        /// </param>
        /// <param name="input">The address of a buffer that contains the new property value. The <paramref name="inputSize"/> parameter contains the size of this buffer.</param>
        /// <param name="inputSize">The size, in bytes, of the <paramref name="input"/> buffer.</param>
        /// <param name="flags">A set of flags that modify the behavior of this function. No flags are defined for this function.</param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        [DllImport(nameof(BCrypt), SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern NTStatus BCryptSetProperty(
            SafeHandle hObject,
            string property,
            [In, MarshalAs(UnmanagedType.LPArray)] byte[] input,
            int inputSize,
            BCryptSetPropertyFlags flags = BCryptSetPropertyFlags.None);

        /// <summary>
        /// Sets the value of a named property for a CNG object.
        /// </summary>
        /// <param name="hObject">A handle that represents the CNG object to set the property value for.</param>
        /// <param name="property">
        /// The name of the property to set. This can be one of the predefined <see cref="PropertyNames"/> or a custom property identifier.
        /// </param>
        /// <param name="input">The new property value. The <paramref name="inputSize"/> parameter contains the size of this buffer.</param>
        /// <param name="inputSize">The size, in bytes, of the <paramref name="input"/> buffer.</param>
        /// <param name="flags">A set of flags that modify the behavior of this function. No flags are defined for this function.</param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        [DllImport(nameof(BCrypt), SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern NTStatus BCryptSetProperty(
            SafeHandle hObject,
            string property,
            string input,
            int inputSize,
            BCryptSetPropertyFlags flags = BCryptSetPropertyFlags.None);

        /// <summary>
        /// Retrieves the value of a named property for a CNG object.
        /// </summary>
        /// <param name="hObject">A handle that represents the CNG object to obtain the property value for.</param>
        /// <param name="property">A pointer to a null-terminated Unicode string that contains the name of the property to retrieve. This can be one of the predefined <see cref="PropertyNames"/> or a custom property identifier.</param>
        /// <param name="output">The address of a buffer that receives the property value. The <paramref name="outputSize"/> parameter contains the size of this buffer.</param>
        /// <param name="outputSize">The size, in bytes, of the <paramref name="output"/> buffer.</param>
        /// <param name="resultSize">A pointer to a ULONG variable that receives the number of bytes that were copied to the pbOutput buffer. If the <paramref name="output"/> parameter is NULL, this function will place the required size, in bytes, in the location pointed to by this parameter.</param>
        /// <param name="flags">A set of flags that modify the behavior of this function. No flags are defined for this function.</param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        [DllImport(nameof(BCrypt), SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern NTStatus BCryptGetProperty(
            SafeHandle hObject,
            string property,
            [Out, MarshalAs(UnmanagedType.LPArray)] byte[] output,
            int outputSize,
            out int resultSize,
            BCryptGetPropertyFlags flags = BCryptGetPropertyFlags.None);

        /// <summary>
        /// Generates a random number.
        /// </summary>
        /// <param name="hAlgorithm">
        /// The handle of an algorithm provider created by using the <see cref="BCryptOpenAlgorithmProvider(string, string, BCryptOpenAlgorithmProviderFlags)"/> function. The algorithm that was specified when the provider was created must support the random number generator interface.
        /// </param>
        /// <param name="pbBuffer">
        /// The address of a buffer that receives the random number. The size of this buffer is specified by the <paramref name="cbBuffer"/> parameter.
        /// </param>
        /// <param name="cbBuffer">
        /// The size, in bytes, of the <paramref name="pbBuffer" /> buffer.
        /// </param>
        /// <param name="flags">A set of flags that modify the behavior of this function. </param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        [DllImport(nameof(BCrypt), SetLastError = true)]
        public static extern NTStatus BCryptGenRandom(
            SafeAlgorithmHandle hAlgorithm,
            byte[] pbBuffer,
            int cbBuffer,
            BCryptGenRandomFlags flags = BCryptGenRandomFlags.None);

        /// <summary>
        /// Closes an algorithm provider.
        /// </summary>
        /// <param name="algorithmHandle">A handle that represents the algorithm provider to close. This handle is obtained by calling the BCryptOpenAlgorithmProvider function.</param>
        /// <param name="flags">A set of flags that modify the behavior of this function. No flags are defined for this function.</param>
        /// <returns>Returns a status code that indicates the success or failure of the function.</returns>
        [DllImport(nameof(BCrypt), SetLastError = true, ExactSpelling = true)]
        private static extern NTStatus BCryptCloseAlgorithmProvider(
            IntPtr algorithmHandle,
            BCryptCloseAlgorithmProviderFlags flags = BCryptCloseAlgorithmProviderFlags.None);

        /// <summary>
        /// Defines the range of key sizes that are supported by the provider.
        /// This structure is used with the <see cref="PropertyNames.KeyLengths"/> property.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct BCRYPT_KEY_LENGTHS_STRUCT
        {
            /// <summary>
            /// The minimum length, in bits, of a key.
            /// </summary>
            public int MinLength;

            /// <summary>
            /// The maximum length, in bits, of a key.
            /// </summary>
            public int MaxLength;

            /// <summary>
            /// The number of bits that the key size can be incremented between dwMinLength and dwMaxLength.
            /// </summary>
            public int Increment;

            /// <summary>
            /// Gets a sequence of allowed key sizes, from smallest to largest.
            /// </summary>
            public IEnumerable<int> KeySizes
            {
                get
                {
                    for (int keyLength = this.MinLength; keyLength <= this.MaxLength; keyLength += this.Increment)
                    {
                        yield return keyLength;
                    }
                }
            }
        }
    }
}