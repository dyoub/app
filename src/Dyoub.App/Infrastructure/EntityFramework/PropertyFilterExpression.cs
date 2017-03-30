// Copyright (c) Dyoub Applications. All rights reserved.
// Licensed under MIT (https://github.com/dyoub/app/blob/master/LICENSE).

using System;
using System.Linq.Expressions;

namespace Dyoub.App.Infrastructure.EntityFramework
{
    public class PropertyFilterExpression<TEntity> where TEntity : class
    {
        private Expression<Func<TEntity, bool>> expression;

        public PropertyFilterExpression(string propertyName, object propertyValue)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(TEntity));
            MemberExpression property = Expression.Property(parameter, propertyName);
            ConstantExpression constant = Expression.Constant(propertyValue);
            BinaryExpression equals = Expression.Equal(property, constant);

            expression = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);
        }

        public static implicit operator Expression<Func<TEntity, bool>>(PropertyFilterExpression<TEntity> filter)
        {
            return filter.expression;
        }
    }
}
