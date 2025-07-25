// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Provisioning.Expressions;
using NUnit.Framework;

namespace Azure.Provisioning.Tests.Expressions
{
    public class IfConditionExpressionTests
    {
        [Test]
        public void CanBeUsedInResourceStatements()
        {
            var param = new ProvisioningParameter("param", typeof(string));
            var resourceStatement = new ResourceStatement(
                "myResource",
                "Microsoft.Storage/storageAccounts@2023-04-01",
                new IfConditionExpression(
                    new BinaryExpression(
                        new IdentifierExpression(param.BicepIdentifier),
                        BinaryBicepOperator.NotEqual,
                        new StringLiteralExpression(string.Empty)),
                    new ObjectExpression(
                        new PropertyExpression("name", new IdentifierExpression(param.BicepIdentifier)),
                        new PropertyExpression("kind", new StringLiteralExpression("StorageV2")))));

            Assert.AreEqual(
                """
                resource myResource 'Microsoft.Storage/storageAccounts@2023-04-01' = if (param != '') {
                  name: param
                  kind: 'StorageV2'
                }

                """, resourceStatement.ToString());
        }

        [Test]
        public void CanBeUsedInArrays()
        {
            var param = new ProvisioningParameter("enableFeature", typeof(bool));
            var arrayExpression = new ArrayExpression(
                new ObjectExpression(
                    new PropertyExpression("name", new StringLiteralExpression("always-item"))),
                new IfConditionExpression(
                    new IdentifierExpression(param.BicepIdentifier),
                    new ObjectExpression(
                        new PropertyExpression("name", new StringLiteralExpression("conditional-item")))));

            Assert.AreEqual(
                """
                [
                  {
                    name: 'always-item'
                  }
                  if (enableFeature) {
                    name: 'conditional-item'
                  }
                ]
                """, arrayExpression.ToString());
        }
    }
}
