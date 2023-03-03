﻿using System.ComponentModel.DataAnnotations;

namespace QuizPop.DAL.Common;

/// <summary>
///     Interface for all entities in the database.
/// </summary>
public interface IEntity
{
    /// <summary>
    ///    The primary key of the entity.
    /// </summary>
    [Key] public int Id { get; init; }
}