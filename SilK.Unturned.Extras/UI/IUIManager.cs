﻿using Autofac;
using Cysharp.Threading.Tasks;
using OpenMod.API.Ioc;
using OpenMod.Unturned.Users;
using System.Collections.Generic;

namespace SilK.Unturned.Extras.UI
{
    /// <summary>
    /// A service which manages players' UI sessions.
    /// </summary>
    [Service]
    public interface IUIManager
    {
        /// <summary>
        /// Starts a UI session. If the session already exists, it will be ended and new session will begin.
        /// To create a new session only if one doesn't exist see <see cref="GetOrStartSession{TSession}"/>.
        /// </summary>
        /// <typeparam name="TSession">The UI session type.</typeparam>
        /// <param name="user">The user co-related with the session.</param>
        /// <param name="options">The options for extra session functionality.</param>
        /// <param name="lifetimeScope">
        /// The scope for controlling service resolution and disposal of the session.
        /// Using the scope of a command or event listener will cause the session to dispose after execution.
        /// </param>
        /// <returns>The new UI session.</returns>
        UniTask<TSession> StartSession<TSession>(UnturnedUser user, UISessionOptions? options = null,
            ILifetimeScope? lifetimeScope = null) where TSession : class, IUISession;

        /// <summary>
        /// Ends a UI session.
        /// </summary>
        /// <typeparam name="TSession">The UI session type.</typeparam>
        /// <param name="user">The user co-related with the session.</param>
        /// <returns><c>true</c> if a session exists and was ended, <c>false</c> otherwise.</returns>
        UniTask<bool> EndSession<TSession>(UnturnedUser user) where TSession : class, IUISession;

        /// <summary>
        /// Ends all UI sessions of a given type.
        /// </summary>
        /// <typeparam name="TSession">The UI session type.</typeparam>
        UniTask EndAllSessions<TSession>() where TSession : class, IUISession;

        /// <summary>
        /// Gets a UI session co-related with the given user.
        /// </summary>
        /// <typeparam name="TSession">The UI session type.</typeparam>
        /// <param name="user">The user co-related with the session.</param>
        /// <returns>The UI session if one exists, <c>null</c> otherwise.</returns>
        UniTask<TSession?> GetSession<TSession>(UnturnedUser user) where TSession : class, IUISession;

        /// <summary>
        /// Gets all UI sessions co-related with a player.
        /// </summary>
        /// <param name="user">The user co-related with the sessions.</param>
        /// <returns>All UI sessions co-related with a player.</returns>
        UniTask<IReadOnlyCollection<IUISession>> GetSessions(UnturnedUser user);

        /// <summary>
        /// Gets a UI session co-related with the given user.
        /// If no session exists, a new session will be started.
        /// </summary>
        /// <typeparam name="TSession">The UI session type.</typeparam>
        /// <param name="user">The user co-related with the session.</param>
        /// <param name="options">The options for extra session functionality.</param>
        /// <param name="lifetimeScope">
        /// The scope for controlling service resolution and disposal of the session.
        /// Using the scope of a command or event listener will cause the session to dispose after execution.
        /// </param>
        /// <returns>The retrieved/created UI session.</returns>
        UniTask<TSession> GetOrStartSession<TSession>(UnturnedUser user, UISessionOptions? options = null,
            ILifetimeScope? lifetimeScope = null) where TSession : class, IUISession;

        /// <summary>
        /// Checks if the user's cursor is enabled.
        /// </summary>
        /// <param name="user">The user who's cursor is being checked.</param>
        /// <returns><c>True</c> if the cursor is enabled; <c>false</c> otherwise.</returns>
        UniTask<bool> IsCursorEnabled(UnturnedUser user);

        /// <summary>
        /// Enables/disables the cursor associated with the given id.
        /// If the cursor is enabled, the player's cursor
        /// will not be disabled until it is disabled for the given id.
        /// </summary>
        /// <param name="user">The user to enable the cursor for.</param>
        /// <param name="id">The id related to the cursor's enablement.</param>
        /// <param name="enabled">Whether or not the cursor is being enabled.</param>
        UniTask SetCursor(UnturnedUser user, string id, bool enabled);
    }
}
