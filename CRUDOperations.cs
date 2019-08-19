using System;
using System.Xml;
using System.Linq;
using System.Data.Services.Client;
using System.Net;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Terrasoft.Nui.ServiceModel.DataContract;
using Terrasoft.Core.Entities;
using System.Web.Script.Serialization;
using Terrasoft.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DataService_BpmOnline_CRUD
{
    class CRUDOperations
    {
        public static void UpdateEntity(Guid accountId, string name, int completness, DateTime createdOn, Guid typeId, string requestMethod)
        {
            var updateQuery = new UpdateQuery()
            {
                RootSchemaName = "Account",
                ColumnValues = new ColumnValues()
                {
                    Items = new Dictionary<string, ColumnExpression>()
                    {
                        {
                            "Name",
                            new ColumnExpression()
                            {
                                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                                Parameter = new Parameter()
                                {
                                    Value = name,
                                    DataValueType = DataValueType.Text
                                }
                            }
                        },
                        {
                            "CreatedOn",
                            new ColumnExpression()
                            {
                                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                                Parameter = new Parameter
                                {
                                    Value = "\"" + createdOn + "\"",
                                    DataValueType = DataValueType.DateTime
                                }
                            }
                        },
                        {
                            "Completeness",
                            new ColumnExpression()
                            {
                                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                                Parameter = new Parameter
                                {
                                    Value = completness,
                                    DataValueType = DataValueType.Integer
                                }
                            }
                        },
                        {
                            "Type",
                            new ColumnExpression()
                            {
                                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                                Parameter = new Parameter
                                {
                                    Value = typeId,
                                    DataValueType = DataValueType.Guid
                                }
                            }
                        }
                    }
                },
                Filters= new Filters()
                {
                    FilterType = Terrasoft.Nui.ServiceModel.DataContract.FilterType.CompareFilter,
                    ComparisonType = FilterComparisonType.Equal,
                    LeftExpression = new BaseExpression()
                    {
                        ExpressionType = EntitySchemaQueryExpressionType.SchemaColumn,
                        ColumnPath = "Id"
                    },
                    RightExpression = new BaseExpression()
                    {
                        ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                        Parameter = new Parameter()
                        {
                            DataValueType = DataValueType.Guid,
                            Value = accountId
                        }
                    }
                }
        };
            var json = new JavaScriptSerializer().Serialize(updateQuery);
            byte[] jsonArray = Encoding.UTF8.GetBytes(json);
            LoginClass.CreateAndSendHttpRequestDataService(out HttpWebRequest updateRequest, Configuration.RequestType.Update);
            using (var requestStream = updateRequest.GetRequestStream())
            {
                requestStream.Write(jsonArray, 0, jsonArray.Length);
            }
            using (var response = (HttpWebResponse)updateRequest.GetResponse())
            {

            }
        }
        public static void CreateEntity(string name, int completness, DateTime createdOn, Guid typeId)
        {
            var insertQuery = new InsertQuery()
            {
                RootSchemaName = "Account",
                ColumnValues = new ColumnValues()
            };

            #region nameColumn
            var columnExpressionName = new ColumnExpression()
            {
                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                Parameter = new Parameter
                {
                    Value = name,
                    DataValueType = DataValueType.Text
                }
            };
            #endregion

            #region createdOnColumn
            var columnExpressionCreatedOn = new ColumnExpression()
            {
                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                Parameter = new Parameter
                {
                    Value = "\"" + createdOn + "\"",
                    DataValueType = DataValueType.DateTime
                }
            };
            #endregion

            #region comletenessColumn
            var columnExpressionCompleteness = new ColumnExpression()
            {
                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                Parameter = new Parameter
                {
                    Value = completness,
                    DataValueType = DataValueType.Integer
                }
            };
            #endregion

            #region accountTypeColumn
            var columnExpressionAccountType = new ColumnExpression()
            {
                ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                Parameter = new Parameter
                {
                    Value = typeId,
                    DataValueType = DataValueType.Guid
                }
            };
            #endregion

            insertQuery.ColumnValues.Items = new Dictionary<string, ColumnExpression>();
            insertQuery.ColumnValues.Items.Add("Name", columnExpressionName);
            insertQuery.ColumnValues.Items.Add("CreatedOn", columnExpressionCreatedOn);
            insertQuery.ColumnValues.Items.Add("Completeness", columnExpressionCompleteness);
            insertQuery.ColumnValues.Items.Add("Type", columnExpressionAccountType);

            var json = new JavaScriptSerializer().Serialize(insertQuery);
            byte[] jsonArray = Encoding.UTF8.GetBytes(json);
            LoginClass.CreateAndSendHttpRequestDataService(out HttpWebRequest insertRequest, Configuration.RequestType.Insert);
            using (var requestStream = insertRequest.GetRequestStream())
            {
                requestStream.Write(jsonArray, 0, jsonArray.Length);
            }
            using (var response = (HttpWebResponse)insertRequest.GetResponse())
            {

            }
        }

        public static void ReadEntity(string probableName, Guid accountId, bool isByGuid)
        {
            var selectQuery = new SelectQuery()
            {
                RootSchemaName = "Account",
                Columns = new SelectQueryColumns()
            };

            #region nameColumn
            var columnExpressionName = new ColumnExpression()
            {
                ExpressionType = EntitySchemaQueryExpressionType.SchemaColumn,
                ColumnPath = "Name"
            };
            var selectQueryColumnName = new SelectQueryColumn()
            {
                Expression = columnExpressionName
            };
            #endregion

            #region createdOnColumn
            var columnExpressionCreatedOn = new ColumnExpression()
            {
                ExpressionType = EntitySchemaQueryExpressionType.SchemaColumn,
                ColumnPath = "CreatedOn"
            };
            var selectQueryColumnCreatedOn = new SelectQueryColumn()
            {
                Expression = columnExpressionCreatedOn
            };
            #endregion

            #region comletenessColumn
            var columnExpressionCompleteness = new ColumnExpression()
            {
                ExpressionType = EntitySchemaQueryExpressionType.SchemaColumn,
                ColumnPath = "Completeness"
            };
            var selectQueryColumnCompleteness = new SelectQueryColumn()
            {
                Expression = columnExpressionCompleteness
            };
            #endregion

            #region zipColumn
            var columnExpressionZip = new ColumnExpression()
            {
                ExpressionType = EntitySchemaQueryExpressionType.SchemaColumn,
                ColumnPath = "Zip"
            };
            var selectQueryColumnZip = new SelectQueryColumn()
            {
                Expression = columnExpressionZip
            };
            #endregion

            #region accountTypeColumn
            var columnExpressionAccountType = new ColumnExpression()
            {
                ExpressionType = EntitySchemaQueryExpressionType.SubQuery,
                ColumnPath = "[AccountType:Id:TypeId].Name",
            };
            var selectQueryColumnAccountType = new SelectQueryColumn()
            {
                Expression = columnExpressionAccountType
            };
            #endregion

            selectQuery.Columns.Items = new Dictionary<string, SelectQueryColumn>()
            {
                {
                    "Name",
                    selectQueryColumnName
                },
                {
                   "CreatedOn",
                   selectQueryColumnCreatedOn
                },
                {
                   "Completeness",
                   selectQueryColumnCompleteness
                },
                {
                   "Zip",
                   selectQueryColumnZip
                },
                {
                   "AccountType",
                   selectQueryColumnAccountType
                },

            };
            var byNameFilter = new Filters()
            {
                FilterType = Terrasoft.Nui.ServiceModel.DataContract.FilterType.CompareFilter,
                ComparisonType = FilterComparisonType.Contain,
                LeftExpression = new BaseExpression()
                {
                    ExpressionType = EntitySchemaQueryExpressionType.SchemaColumn,
                    ColumnPath = "Name"
                },
                RightExpression = new BaseExpression()
                {
                    ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                    Parameter = new Parameter()
                    {
                        DataValueType = DataValueType.Text,
                        Value = probableName
                    }
                },
            };

            var byIdFilter = new Filters()
            {
                FilterType = Terrasoft.Nui.ServiceModel.DataContract.FilterType.CompareFilter,
                ComparisonType = FilterComparisonType.Equal,
                LeftExpression = new BaseExpression()
                {
                    ExpressionType = EntitySchemaQueryExpressionType.SchemaColumn,
                    ColumnPath = "Id"
                },
                RightExpression = new BaseExpression()
                {
                    ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                    Parameter = new Parameter()
                    {
                        DataValueType = DataValueType.Guid,
                        Value = accountId
                    }
                }
            };
            if (isByGuid)
                selectQuery.Filters = byIdFilter;
            else
                selectQuery.Filters = byNameFilter;


            var json = new JavaScriptSerializer().Serialize(selectQuery);
            byte[] jsonArray = Encoding.UTF8.GetBytes(json);
            LoginClass.CreateAndSendHttpRequestDataService(out HttpWebRequest selectRequest, Configuration.RequestType.Select);
            using (var requestStream = selectRequest.GetRequestStream())
            {
                requestStream.Write(jsonArray, 0, jsonArray.Length);
            }
            string jsonanswer;
            using (var response = (HttpWebResponse)selectRequest.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    jsonanswer = reader.ReadToEnd();
                }
            }

            RootObject jsonObject = JsonConvert.DeserializeObject<RootObject>(jsonanswer);
            foreach (var item in jsonObject.rows)
            {
                Console.WriteLine(String.Format(Localize.AccountOutput, item.GetPropertyValue("Name"), item.GetPropertyValue("AccountType"), item.GetPropertyValue("Zip"), item.GetPropertyValue("Completeness"), item.GetPropertyValue("CreatedOn")));
            }


        }
        public static void DeleteEntity(Guid accountId)
        {
            var byIdFilter = new Filters()
            {
                FilterType = Terrasoft.Nui.ServiceModel.DataContract.FilterType.CompareFilter,
                ComparisonType = FilterComparisonType.Equal,
                LeftExpression = new BaseExpression()
                {
                    ExpressionType = EntitySchemaQueryExpressionType.SchemaColumn,
                    ColumnPath = "Id"
                },
                RightExpression = new BaseExpression()
                {
                    ExpressionType = EntitySchemaQueryExpressionType.Parameter,
                    Parameter = new Parameter()
                    {
                        DataValueType = DataValueType.Guid,
                        Value = accountId
                    }
                }
            };
            var deleteQuery = new DeleteQuery()
            {
                RootSchemaName = "Account",

                Filters = byIdFilter
            };
            var json = new JavaScriptSerializer().Serialize(deleteQuery);
            byte[] jsonArray = Encoding.UTF8.GetBytes(json);
            LoginClass.CreateAndSendHttpRequestDataService(out HttpWebRequest deleteRequest, Configuration.RequestType.Delete);
            using (var requestStream = deleteRequest.GetRequestStream())
            {
                requestStream.Write(jsonArray, 0, jsonArray.Length);
            }
            using (var response = (HttpWebResponse)deleteRequest.GetResponse())
            {
            }
        }
    }
}
