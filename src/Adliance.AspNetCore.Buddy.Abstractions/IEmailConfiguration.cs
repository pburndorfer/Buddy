﻿// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
namespace Adliance.AspNetCore.Buddy.Abstractions
{
    /// <summary>
    /// Specifies the contract of a configuration for an email provider.
    /// </summary>
    public interface IEmailConfiguration
    {
        /// <summary>
        /// The name of the sender.
        /// </summary>
        string SenderName { get; }

        /// <summary>
        /// The email address of the sender.
        /// </summary>
        string SenderAddress { get; }

        /// <summary>
        /// The "reply to" address (can be different from the sender address.
        /// </summary>
        string ReplyToAddress { get; }
    }

    // ReSharper disable once UnusedType.Global
    /// <summary>
    /// A default email configuration class.
    /// </summary>
    public class DefaultEmailConfiguration : IEmailConfiguration
    {
        /// <inheritdoc cref="IEmailConfiguration.SenderName"/>
        public string SenderName { get; set; } = string.Empty;
        
        /// <inheritdoc cref="IEmailConfiguration.SenderAddress"/>
        public string SenderAddress { get; set; } = string.Empty;
        
        /// <inheritdoc cref="IEmailConfiguration.ReplyToAddress"/>
        public string ReplyToAddress { get; set; } = string.Empty;
    }
}
