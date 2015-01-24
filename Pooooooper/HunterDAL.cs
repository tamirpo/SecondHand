using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HunterMVC.Models;
using System.Data.SqlClient;
using System.Data;
using HunterMVC.Models.Enums;

namespace HunterMVC
{
    public class HunterDAL
    {
        String streetsTableNameTelAviv = "tbl_streets_neighborhoods";
        String streetsAliasesTableNameTelAviv = "streetsAliases";
        String neighborhoodsAliasesTableNameTelAviv = "neighborhoodsAliases";
        String neighborhoodsTableNameTelAviv = "neighborhoods";
        String areasAliasesTableNameTelAviv = "areasAliases";
        String areasTableNameTelAviv = "areas";
        String subAreasTableNameTelAviv = "subAreas";
        String locationsAliasesTableNameTelAviv = "locationsAliases";
        String locationsTableNameTelAviv = "locations";
        String locationsToNeighborhoodsTableNameTelAviv = "locations_streets_neighborhoods";

        String streetsTableNameRamatGan = "streetsRamatGan";
        String streetsAliasesTableNameRamatGan = "streetsRamatGanAliases";
        String neighborhoodsAliasesTableNameRamatGan = "neighborhoodsRamatGanAliases";
        String neighborhoodsTableNameRamatGan = "neighborhoodsRamatGan";
        String areasAliasesTableNameRamatGan = "areasRamatGanAliases";
        String areasTableNameRamatGan = "areasRamatGan";
        String locationsAliasesTableNameRamatGan = "locationsRamatGanAliases";
        String locationsTableNameRamatGan = "locationsRamatGan";
        String subAreasTableNameRamatGan = "subAreasRamatGan";
        String locationsToNeighborhoodsTableNameRamatGan = "locations_streets_neighborhoodsRamatGan";

        String streetsTableNameGivataim = "streetsGivataim";
        String streetsAliasesTableNameGivataim = "streetsGivataimAliases";
        String neighborhoodsAliasesTableNameGivataim = "neighborhoodsGivataimAliases";
        String neighborhoodsTableNameGivataim = "neighborhoodsGivataim";
        String areasAliasesTableNameGivataim = "areasGivataimAliases";
        String areasTableNameGivataim = "areasGivataim";
        String locationsAliasesTableNameGivataim = "locationsGivataimAliases";
        String locationsTableNameGivataim = "locationsGivataim";
        String subAreasTableNameGivataim = "subAreasGivataim";
        String locationsToNeighborhoodsTableNameGivataim = "locations_streets_neighborhoodsGivataim";

        SqlConnection connection;
        public HunterDAL()
        {
            connection = new SqlConnection(@"Data Source=secondhand.crfixukv0lom.eu-west-1.rds.amazonaws.com;
                                            Initial Catalog=secondhandtest;Persist Security Info=True;
                                            User ID=secondhand;Password=secondhand");
        }

        public Boolean SaveExpressions(Dictionary<String, String> expressionsVsRootExpressions)
        {
            SqlDataReader reader = null ;
            foreach (String postWord in expressionsVsRootExpressions.Keys)
            {
                try
                {
                    connection.Open();

                    // create a SqlCommand object for this connection
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;

                    command.CommandText += "insert into expressions (name, rootExpression) values (@PostExpression, @RootExpression); ";

                    command.Parameters.Add("@PostExpression", SqlDbType.VarChar);
                    command.Parameters["@PostExpression"].Value = postWord;

                    command.Parameters.Add("@RootExpression", SqlDbType.VarChar);
                    command.Parameters["@RootExpression"].Value = expressionsVsRootExpressions[postWord];

                    // execute the command that returns a SqlDataReader
                    reader = command.ExecuteReader();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    connection.Close();
                }
            }

            return true;
        }

        internal Post GetNewPost()
        {
            return null;
        }

        internal House GetNewHouse(int cityId)
        {
            House result = null;
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select h.*, p.*
                                    from houses h,
                                    (select top 1 * from posts where verified = 0 and id in (select postId from houses where cityid=@CityId) order by datecreated desc) p
                                    where h.postId = p.id ";

            /*command.CommandText = @"Select h.*, p.*
                                    from houses h,
                                    posts p
                                    where h.postId = p.id and h.postid=64865 ";*/

            command.CommandType = CommandType.Text;
            command.Parameters.Add("@CityId", SqlDbType.Int);
            command.Parameters["@CityId"].Value = cityId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            while (reader.Read())
            {
                if (result == null)
                {
                    result = new House(reader["text"].ToString());
                }

                result.PostId = (int)reader["postId"];
                result.CityId = (int)reader["cityId"];
                result.RoomsNumber = (double)reader["roomsnumber"];
                result.RoommatesNumber = (int)reader["totalRoommatesnumber"];
                result.SideId = (int)reader["sideId"];
                result.ParkingId = (int)reader["parkingId"];
                result.RenovatedId = (int)reader["renovatedId"];
                result.PurposeId = (int)reader["purposeId"];
                result.Floor = (int)reader["floor"];
                result.TypeId = (int)reader["typeId"];
                result.id = (int)reader["id"];
                result.Size = (int)reader["size"];
                result.Price = (int)reader["price"];
                result.HouseNumber = (int)reader["houseNumber"];
                result.Comment = reader["comment"].ToString();
                result.SmokeId = (int)reader["smokeId"];
                result.PetsId = (int)reader["petsId"];
                result.ElevatorId = (int)reader["elevatorId"];
                result.BalconyId = (int)reader["balconyId"];
                result.FurnituredId = (int)reader["furnituredId"];
                result.SubletId = (int)reader["subletId"];
                result.FromAgencyId = (int)reader["fromAgencyId"];
                result.StreetsComment = reader["streetComment"].ToString();
                result.PhoneNumber = reader["phoneNumber"].ToString();
                result.StreetsFound = reader["streetsFound"].ToString();
                result.LocationsFound = reader["locationsFound"].ToString();
                result.NeighborhoodsFound = reader["neighborhoodsFound"].ToString();
                result.SubAreasFound = reader["subareasFound"].ToString();
                result.AreasFound = reader["areasFound"].ToString();
                result.AddressesIds.Add(reader["addressConclusionId"].ToString());

            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }



        internal string GetCityFromId(int cityId)
        {
            string cityName = null;
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select name from cities where id = @Id";

            command.CommandType = CommandType.Text;
            command.Parameters.Add("@Id", SqlDbType.Int);
            command.Parameters["@Id"].Value = cityId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                cityName = reader["name"].ToString();


            }

            // close the connection
            reader.Close();
            connection.Close();

            return cityName;
        }

        internal string GetRootExpressionNameFromRootExpressionId(double rootExpressionId)
        {
            string root = "";
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select name from root_expressions where id = @Id";

            command.CommandType = CommandType.Text;
            command.Parameters.Add("@Id", SqlDbType.VarChar);
            command.Parameters["@Id"].Value = rootExpressionId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                root = reader["name"].ToString();

            }

            // close the connection
            reader.Close();
            connection.Close();

            return root;
        }

        internal string GetStreetsTableName(int cityId){
            switch (cityId)
	        {
                case (int)City.TelAviv:
                    return streetsTableNameTelAviv;
                case (int)City.Givataim:
                    return streetsTableNameGivataim;
                case (int)City.RamatGan:
                    return streetsTableNameRamatGan;
		        default:
                    return streetsTableNameTelAviv; 
	        }
        }

        internal string GetStreetsAliasesTableName(int cityId)
        {
            switch (cityId)
            {
                case (int)City.TelAviv:
                    return streetsAliasesTableNameTelAviv;
                case (int)City.Givataim:
                    return streetsAliasesTableNameGivataim;
                case (int)City.RamatGan:
                    return streetsAliasesTableNameRamatGan;
                default:
                    return streetsAliasesTableNameTelAviv;
            }
        }

        internal string GetLocationsToNeighborhoodsTableName(int cityId)
        {
            switch (cityId)
            {
                case (int)City.TelAviv:
                    return locationsToNeighborhoodsTableNameTelAviv;
                case (int)City.Givataim:
                    return locationsToNeighborhoodsTableNameGivataim;
                case (int)City.RamatGan:
                    return locationsToNeighborhoodsTableNameRamatGan;
                default:
                    return locationsToNeighborhoodsTableNameTelAviv;
            }
        }

        internal string GetLocationsTableName(int cityId)
        {
            switch (cityId)
            {
                case (int)City.TelAviv:
                    return locationsTableNameTelAviv;
                case (int)City.Givataim:
                    return locationsTableNameGivataim;
                case (int)City.RamatGan:
                    return locationsTableNameRamatGan;
                default:
                    return locationsTableNameTelAviv;
            }
        }

        internal string GetLocationsAliasesTableName(int cityId)
        {
            switch (cityId)
            {
                case (int)City.TelAviv:
                    return locationsAliasesTableNameTelAviv;
                case (int)City.Givataim:
                    return locationsAliasesTableNameGivataim;
                case (int)City.RamatGan:
                    return locationsAliasesTableNameRamatGan;
                default:
                    return locationsAliasesTableNameTelAviv;
            }
        }

        internal string GetNeighborhoodsTableName(int cityId)
        {
            switch (cityId)
            {
                case (int)City.TelAviv:
                    return neighborhoodsTableNameTelAviv;
                case (int)City.Givataim:
                    return neighborhoodsTableNameGivataim;
                case (int)City.RamatGan:
                    return neighborhoodsTableNameRamatGan;
                default:
                    return neighborhoodsTableNameTelAviv;
            }
        }

        internal string GetNeighborhoodsAliasesTableName(int cityId)
        {
            switch (cityId)
            {
                case (int)City.TelAviv:
                    return neighborhoodsAliasesTableNameTelAviv;
                case (int)City.Givataim:
                    return neighborhoodsAliasesTableNameGivataim;
                case (int)City.RamatGan:
                    return neighborhoodsAliasesTableNameRamatGan;
                default:
                    return neighborhoodsAliasesTableNameTelAviv;
            }
        }

        internal string GetAreasAliasesTableName(int cityId)
        {
            switch (cityId)
            {
                case (int)City.TelAviv:
                    return areasAliasesTableNameTelAviv;
                case (int)City.Givataim:
                    return areasAliasesTableNameGivataim;
                case (int)City.RamatGan:
                    return areasAliasesTableNameRamatGan;
                default:
                    return areasAliasesTableNameTelAviv;
            }
        }

        internal string GetAreasTableName(int cityId)
        {
            switch (cityId)
            {
                case (int)City.TelAviv:
                    return areasTableNameTelAviv;
                case (int)City.Givataim:
                    return areasTableNameGivataim;
                case (int)City.RamatGan:
                    return areasTableNameRamatGan;
                default:
                    return areasTableNameTelAviv;
            }
        }

        internal string GetSubAreasTableName(int cityId)
        {
            switch (cityId)
            {
                case (int)City.TelAviv:
                    return subAreasTableNameTelAviv;
                case (int)City.Givataim:
                    return subAreasTableNameGivataim;
                case (int)City.RamatGan:
                    return subAreasTableNameRamatGan;
                default:
                    return areasTableNameTelAviv;
            }
        }

        internal string GetStreetNameFromId(int streetId, int cityId)
        {
            String streetsTableName = GetStreetsTableName(cityId);

            String streetIdField = "id";
            if (cityId == 1) // TEL AVIV
            {
                streetIdField = "streetId";
            }

            string streetName = null;
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select sn.name from " + streetsTableName + " sn where  sn." + streetIdField + " = @Id";

            command.CommandType = CommandType.Text;
            command.Parameters.Add("@Id", SqlDbType.Int);
            command.Parameters["@Id"].Value = streetId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                streetName = reader["name"].ToString();


            }

            // close the connection
            reader.Close();
            connection.Close();

            return streetName;
        }

        internal string GetStreetAliasNameFromAliasId(int streetId, int cityId)
        {
            String streetsAliasesTableName = GetStreetsAliasesTableName(cityId);

            string streetName = null;
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select name from " + streetsAliasesTableName + " where id = @Id";

            command.CommandType = CommandType.Text;
            command.Parameters.Add("@Id", SqlDbType.Int);
            command.Parameters["@Id"].Value = streetId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                streetName = reader["name"].ToString();


            }

            // close the connection
            reader.Close();
            connection.Close();

            return streetName;
        }


        internal bool UpdateHouseFields(Dictionary<int, int> rootExpressionIdsVsCategoryIds, int houseId)
        {
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                // create a SqlCommand object for this connection
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;


                /*int cityId = 0;
                int streetId = 0;
                int areaId = 0;
                int floorId = 0;
                int roomsNumberId = 0;
                int totalRoommatesNumberId = 0;
                int parkingId = 0;
                int conditionId = 0;
                int purposeId = 0;
                int sideId = 0;
                int typeId = 0;


                foreach (int currentExpressionId in rootExpressionIdsVsCategoryIds.Keys)
                {
                    if (rootExpressionIdsVsCategoryIds[currentExpressionId].Equals((int)CategoryTypes.Area))
                    {
                        areaId = currentExpressionId;
                    }
                    if (rootExpressionIdsVsCategoryIds[currentExpressionId].Equals((int)CategoryTypes.City))
                    {
                        cityId = currentExpressionId;
                    }
                    if (rootExpressionIdsVsCategoryIds[currentExpressionId].Equals((int)CategoryTypes.Street))
                    {
                        streetId = currentExpressionId;
                    }
                    if (rootExpressionIdsVsCategoryIds[currentExpressionId].Equals((int)CategoryTypes.Floor))
                    {
                        floorId = currentExpressionId;
                    }
                    if (rootExpressionIdsVsCategoryIds[currentExpressionId].Equals((int)CategoryTypes.RoomsNumber))
                    {
                        roomsNumberId = currentExpressionId;
                    }
                    if (rootExpressionIdsVsCategoryIds[currentExpressionId].Equals((int)CategoryTypes.RoommatesNumber))
                    {
                        totalRoommatesNumberId = currentExpressionId;
                    }
                    if (rootExpressionIdsVsCategoryIds[currentExpressionId].Equals((int)CategoryTypes.Side))
                    {
                        sideId = currentExpressionId;
                    }
                    if (rootExpressionIdsVsCategoryIds[currentExpressionId].Equals((int)CategoryTypes.Purpose))
                    {
                        purposeId = currentExpressionId;
                    }
                    if (rootExpressionIdsVsCategoryIds[currentExpressionId].Equals((int)CategoryTypes.Type))
                    {
                        typeId = currentExpressionId;
                    }
                    if (rootExpressionIdsVsCategoryIds[currentExpressionId].Equals((int)CategoryTypes.Parking))
                    {
                        parkingId = currentExpressionId;
                    }
                    if (rootExpressionIdsVsCategoryIds[currentExpressionId].Equals((int)CategoryTypes.Condition))
                    {
                        conditionId = currentExpressionId;
                    }
                }


                command.CommandText = @"update houses set cityId = @CityId, areaId = @AreaId, totalRoommatesNumberId = @TotalRoommatesNumberId , 
                                        roomsNumberId = @RoomsNumberId, floorId = @FloorId, sideId = @SideId, conditionId = @ConditionId, 
                                        purposeId = @PurposeId, parkingId = @ParkingId, typeId = @TypeId ,
                                        verified = 1
                                        where id = @Id";

                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters["@Id"].Value = houseId;

                command.Parameters.Add("@CityId", SqlDbType.Int);
                command.Parameters["@CityId"].Value = cityId;

                command.Parameters.Add("@AreaId", SqlDbType.Int);
                command.Parameters["@AreaId"].Value = areaId;

                command.Parameters.Add("@StreetId", SqlDbType.Int);
                command.Parameters["@StreetId"].Value = streetId;

                command.Parameters.Add("@FloorId", SqlDbType.Int);
                command.Parameters["@FloorId"].Value = floorId;

                command.Parameters.Add("@SideId", SqlDbType.Int);
                command.Parameters["@SideId"].Value = sideId;

                command.Parameters.Add("@ConditionId", SqlDbType.Int);
                command.Parameters["@ConditionId"].Value = conditionId;

                command.Parameters.Add("@PurposeId", SqlDbType.Int);
                command.Parameters["@PurposeId"].Value = purposeId;

                command.Parameters.Add("@TypeId", SqlDbType.Int);
                command.Parameters["@TypeId"].Value = typeId;

                command.Parameters.Add("@RoomsNumberId", SqlDbType.Int);
                command.Parameters["@RoomsNumberId"].Value = roomsNumberId;

                command.Parameters.Add("@TotalRoommatesNumberId", SqlDbType.Int);
                command.Parameters["@TotalRoommatesNumberId"].Value = totalRoommatesNumberId;

                command.Parameters.Add("@ParkingId", SqlDbType.Int);
                command.Parameters["@ParkingId"].Value = parkingId;

   */
                command.CommandText = @"update houses set verified = 1
                                        where id = @Id";

                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters["@Id"].Value = houseId;

                // execute the command that returns a SqlDataReader
                reader = command.ExecuteReader();

                // display the results
                if (reader.RecordsAffected == 1)
                {
                    return true;;
                }
                else{
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                // close the connection
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                connection.Close();

            }
        }

        internal Dictionary<string, string> GetRootExpressionsIdsVsCategoriesIds(int cityId)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select r.id rootExpressionsId, c.id categoryId from root_expressions r, categories c where c.id = r.categoryId and  c.isInEngineUI = 1 ";
                                    

            command.CommandType = CommandType.Text;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            while (reader.Read())
            {
                string rootExpressionId = reader["rootExpressionsId"].ToString();
                string categoryId = reader["categoryId"].ToString();
                result.Add(rootExpressionId, categoryId);
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal Dictionary<string, List<string>> GetCategoriesIdsVsAddressesIds(int cityId)
        {
            Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
            connection.Open();

            string streetsTableName = GetStreetsTableName(cityId);
            string neighborhoodTableName = GetNeighborhoodsTableName(cityId);
            string subAreasTableName = GetSubAreasTableName(cityId);
            string locationsTableName = GetLocationsTableName(cityId);
            string streetIdColumn = "id";
            if (cityId == 1)
            {
                streetIdColumn = "streetId";
            }
            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"
                                    Select " + streetIdColumn + @" rootExpressionsId, 27 categoryId
                                    from " + streetsTableName + @" r
                                    Union 
                                    Select id rootExpressionsId, 32 categoryId
                                    from " + neighborhoodTableName + @" r
                                    Union 
                                    Select id rootExpressionsId, 44 categoryId
                                    from " + subAreasTableName + @" r
                                    Union 
                                    Select id rootExpressionsId, 35 categoryId
                                    from " + locationsTableName + @" r";


            command.CommandType = CommandType.Text;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            while (reader.Read())
            {
                string rootExpressionId = reader["rootExpressionsId"].ToString();
                string categoryId = reader["categoryId"].ToString();
                if (!result.ContainsKey(categoryId))
                {
                    List<string> addresses = new List<string>();
                    addresses.Add(rootExpressionId);
                    result.Add(categoryId, addresses);
                }
                else
                {
                    result[categoryId].Add(rootExpressionId);
                }
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal Dictionary<string, string> GetAllCategories()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select name, id from categories where isInEngineUI = 1";

            command.CommandType = CommandType.Text;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            while (reader.Read())
            {
                string id = reader["id"].ToString();
                string name = reader["name"].ToString();
                result.Add(id, name);
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal Dictionary<string, string> GetAllRootExpressions()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select name, id from root_expressions";

            command.CommandType = CommandType.Text;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            while (reader.Read())
            {
                string id = reader["id"].ToString();
                string name = reader["name"].ToString();
                result.Add(id, name);
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal Dictionary<int, int> GetExpressionsIds(Dictionary<string, string>.KeyCollection expressions)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            string expressionsString = "";
            foreach (string currentExpression in expressions)
            {
                expressionsString += "'"+ currentExpression +"',";
            }
            expressionsString = expressionsString.Substring(0, expressionsString.Length - 1);

            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select e.id expressionId, c.id categoryId 
                                    from expressions e, root_expressions r, categories c
                                    where e.rootExpression = r.name and r.categoryId = c.id
                                    and e.name in (" + expressionsString + ")";

            command.CommandType = CommandType.Text;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            while (reader.Read())
            {
                int expressionId = (int)reader["expressionId"];
                int categoryId = (int)reader["categoryId"];
                result.Add(expressionId, categoryId);
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal bool VerifyHouse(int houseId)
        {
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                // create a SqlCommand object for this connection
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                command.CommandText = @"update posts set verified = 1
                                        where id in (select distinct postid from houses where id = @Id)";

                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters["@Id"].Value = houseId;

                // execute the command that returns a SqlDataReader
                reader = command.ExecuteReader();

                // display the results
                if (reader.RecordsAffected == 1)
                {
                    return true; ;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                // close the connection
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                connection.Close();

            }
        }

        internal string GetCityNameFromId(int cityId)
        {
            string result = "";
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select name from cities where id = @CityId";

            command.CommandType = CommandType.Text;

            command.Parameters.Add("@CityId", SqlDbType.Int);
            command.Parameters["@CityId"].Value = cityId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                result = reader["name"].ToString();
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal string GetCityFromAliasId(int cityAliasId)
        {
            string result = "";
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select c.name from citiesAliases ca, cities c where c.id = ca.cityId and ca.id = @CityAliasId";

            command.CommandType = CommandType.Text;

            command.Parameters.Add("@CityAliasId", SqlDbType.Int);
            command.Parameters["@CityAliasId"].Value = cityAliasId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                result = reader["name"].ToString();
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal string GetNeighborhoodAliasFromId(int neighborhoodId, int cityId)
        {
            String neighborhoodsAliasesTableName = GetNeighborhoodsAliasesTableName(cityId);

            string result = "";
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select name from " + neighborhoodsAliasesTableName + " where id = @NeighborhoodyId";

            command.CommandType = CommandType.Text;

            command.Parameters.Add("@NeighborhoodyId", SqlDbType.Int);
            command.Parameters["@NeighborhoodyId"].Value = neighborhoodId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                result = reader["name"].ToString();
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal string GetNeighborhoodFromAliasId(int neighborhoodId, int cityId)
        {
            String neighborhoodsAliasesTableName = GetNeighborhoodsAliasesTableName(cityId);
            String neighborhoodsTableName = GetNeighborhoodsTableName(cityId);

            string result = "";
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select c.name from " + neighborhoodsAliasesTableName + " ca, " + neighborhoodsTableName + " c where c.id = ca.neighborhoodid and ca.id = @NeighborhoodAliasId";

            command.CommandType = CommandType.Text;

            command.Parameters.Add("@NeighborhoodAliasId", SqlDbType.Int);
            command.Parameters["@NeighborhoodAliasId"].Value = neighborhoodId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                result = reader["name"].ToString();
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal string GetAreaAliasFromId(int areaId, int cityId)
        {
            String areasAliasesTableName = GetAreasAliasesTableName(cityId);

            string result = "";
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select name from " + areasAliasesTableName + " where id = @AreaId";

            command.CommandType = CommandType.Text;

            command.Parameters.Add("@AreaId", SqlDbType.Int);
            command.Parameters["@AreaId"].Value = areaId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                result = reader["name"].ToString();
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal string GetAreaFromAliasId(int areaId, int cityId)
        {
            String areasAliasesTableName = GetAreasAliasesTableName(cityId);
            String areasTableName = GetAreasTableName(cityId);


            string result = "";
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select c.name from " + areasAliasesTableName + " ca, " + areasTableName + " c where c.id = ca.areaid and ca.id = @AreaAliasId";

            command.CommandType = CommandType.Text;

            command.Parameters.Add("@AreaAliasId", SqlDbType.Int);
            command.Parameters["@AreaAliasId"].Value = areaId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                result = reader["name"].ToString();
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal string GetLocationNameFromId(int locationId, int cityId)
        {
            String locationsTableName = GetLocationsTableName(cityId);

            string result = "";
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select l.name from " + locationsTableName + " l where l.id = @LocationId";

            command.CommandType = CommandType.Text;

            command.Parameters.Add("@LocationId", SqlDbType.Int);
            command.Parameters["@LocationId"].Value = locationId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                result = reader["name"].ToString();
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal string GetSubAreaNameFromId(int id, int cityId)
        {
            String subAreasTableName = GetSubAreasTableName(cityId);

            string result = "";
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select name from " + subAreasTableName + " where id = @Id";

            command.CommandType = CommandType.Text;

            command.Parameters.Add("@Id", SqlDbType.Int);
            command.Parameters["@Id"].Value = id;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                result = reader["name"].ToString();
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal string GetLocationAliasNameFromId(int locationId, int cityId)
        {
            String locationsAliasesTableName = GetLocationsAliasesTableName(cityId);

            string result = "";
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select name from " + locationsAliasesTableName + " where id = @LocationAliasId";

            command.CommandType = CommandType.Text;

            command.Parameters.Add("@LocationAliasId", SqlDbType.Int);
            command.Parameters["@LocationAliasId"].Value = locationId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                result = reader["name"].ToString();
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal string GetNeighborhoodFromId(int neighborhoodId, int cityId)
        {
            String neighborhoodsTableName = GetNeighborhoodsTableName(cityId);


            string result = "";
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select name from " + neighborhoodsTableName + " where id = @NeighborhoodId";

            command.CommandType = CommandType.Text;

            command.Parameters.Add("@NeighborhoodId", SqlDbType.Int);
            command.Parameters["@NeighborhoodId"].Value = neighborhoodId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                result = reader["name"].ToString();
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal string GetAreaFromId(int areaId, int cityId)
        {
            String areasTableName = GetAreasTableName(cityId);


            string result = "";
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select name from " + areasTableName + " where id = @AreaId";

            command.CommandType = CommandType.Text;

            command.Parameters.Add("@AreaId", SqlDbType.Int);
            command.Parameters["@AreaId"].Value = areaId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                result = reader["name"].ToString();
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal void AddBug(int houseId, string comment)
        {
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                // create a SqlCommand object for this connection
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                command.CommandText = @"insert into bugs (houseId,comment) values (@Id,@Comment)";

                command.Parameters.Add("@Id", SqlDbType.Int);
                command.Parameters["@Id"].Value = houseId;

                command.Parameters.Add("@Comment", SqlDbType.NVarChar);
                command.Parameters["@Comment"].Value = comment;

                // execute the command that returns a SqlDataReader
                reader = command.ExecuteReader();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                // close the connection
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                connection.Close();

            }
        }

        internal string LoginUser(string imei)
        {
            String result = String.Empty;
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                // create a SqlCommand object for this connection
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                String accessToken = Guid.NewGuid().ToString();

                command.CommandText = @"
                                        IF EXISTS (SELECT * FROM users_tokens WHERE userId=@IMEI)
                                            UPDATE users_tokens SET accessToken=@AccessToken WHERE userId=@IMEI
                                        ELSE
                                            INSERT INTO users_tokens (userId,accessToken) VALUES (@IMEI, @AccessToken)";

                command.Parameters.Add("@IMEI", SqlDbType.NVarChar);
                command.Parameters["@IMEI"].Value = imei;

                command.Parameters.Add("@AccessToken", SqlDbType.NVarChar);
                command.Parameters["@AccessToken"].Value = accessToken;

                // execute the command that returns a SqlDataReader
                reader = command.ExecuteReader();

                result = accessToken;

            }
            catch (Exception ex)
            {

            }
            finally
            {
                // close the connection
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                connection.Close();

            }

            return result;
            
        }

        internal bool VerifyUserLogin(string token, string imei)
        {
            bool result = false; 

            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select count(*) userCount from users_tokens where userId = @UserId and accesstoken = @AccessToken";

            command.CommandType = CommandType.Text;

            command.Parameters.Add("@UserId", SqlDbType.NVarChar);
            command.Parameters["@UserId"].Value = imei;
            command.Parameters.Add("@AccessToken", SqlDbType.NVarChar);
            command.Parameters["@AccessToken"].Value = token;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                int userCount = (int)reader["userCount"];
                if (userCount > 0)
                {
                    result = true;
                }
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal void AttachPostsImages(ref List<House> houses)
        {
            List<House> result = new List<House>();

            connection.Open();

            string inArray = String.Empty;
            foreach (House currentHouse in houses)
            {
                inArray += "'" + currentHouse.PostId + "',";
            }
            inArray = inArray.Substring(0, inArray.Length - 1);

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            String query = @"Select * from posts_images where postid in (" + inArray + ")";

            command.CommandText = query;
            command.CommandType = CommandType.Text;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            House house = new House();
            while (reader.Read())
            {
                int currentPostId = (int)reader["postid"];
                IEnumerable<House> currentHouses = houses.Where(o=>o.PostId == currentPostId);
                foreach (House currentHouse in currentHouses)
                {
                    currentHouse.ImageUrls.Add(reader["image_url"].ToString());
                }
            }

            // close the connection
            reader.Close();
            connection.Close();
        }

        internal List<House> GetApartments(UserSearch userSearch, DateTime startDate)
        {
            return GetApartments(userSearch, startDate, DateTime.MaxValue);
        }

        internal List<House> GetApartments(UserSearch userSearch, DateTime startDate, DateTime endDate)
        {
            String subAreasTableName = GetSubAreasTableName(userSearch.City);

            List<House> result = new List<House>();

            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            String query = @"Select distinct h.*,p.*,sa.areaId realAreaId 
                            from houses h, posts p , houseAddressConclusions ha, " + subAreasTableName + @"  sa
                                where p.id=h.postid and h.addressConclusionId = ha.id and sa.id=ha.objectId
                                and ha.objectType=1
                                and h.id in (
                                select  max(h.id) maxHouseId
                                from houses h, posts p
                                where h.postId  = p.id 
                                and purposeId = @PurposeId 
                                and (h.dateCreated between @startDate and @endDate)
                                group by sender,h.cityId,purposeId, addressConclusionId) ";

            command.Parameters.Add("@PurposeId", SqlDbType.Int);
            command.Parameters["@PurposeId"].Value = userSearch.Purpose;

            command.Parameters.Add("@startDate", SqlDbType.DateTime);
            command.Parameters["@startDate"].Value = startDate;

            command.Parameters.Add("@endDate", SqlDbType.DateTime);
            command.Parameters["@endDate"].Value = endDate;

            if (userSearch.FromSize > 0)
            {
                query += " and ((size between @FromSize and @ToSize) or size=0) ";
                command.Parameters.Add("@FromSize", SqlDbType.Int);
                command.Parameters["@FromSize"].Value = userSearch.FromSize;
                command.Parameters.Add("@ToSize", SqlDbType.Int);
                command.Parameters["@ToSize"].Value = userSearch.ToSize;
            }

            if (userSearch.FromPrice > 0)
            {
                query += " and ((price between @FromPrice and @ToPrice) or price=0) ";
                command.Parameters.Add("@FromPrice", SqlDbType.Int);
                command.Parameters["@FromPrice"].Value = userSearch.FromPrice;
                command.Parameters.Add("@ToPrice", SqlDbType.Int);
                command.Parameters["@ToPrice"].Value = userSearch.ToPrice;
            }

            if (userSearch.FromRoomsNumber > 0)
            {
                query += " and (roomsNumber between @FromRoomsNumber and @ToRoomsNumber) ";
                command.Parameters.Add("@FromRoomsNumber", SqlDbType.Decimal);
                command.Parameters["@FromRoomsNumber"].Value = userSearch.FromRoomsNumber;
                command.Parameters.Add("@ToRoomsNumber", SqlDbType.Decimal);
                command.Parameters["@ToRoomsNumber"].Value = userSearch.ToRoomsNumber;
            }

            if (userSearch.FromTotalRoommatesNumber > 0)
            {
                query += " and ((totalRoommatesNumber between @FromTotalRoommatesNumber and @ToTotalRoommatesNumber) or totalRoommatesNumber=0) ";
                command.Parameters.Add("@FromTotalRoommatesNumber", SqlDbType.Int);
                command.Parameters["@FromTotalRoommatesNumber"].Value = userSearch.FromTotalRoommatesNumber;
                command.Parameters.Add("@ToTotalRoommatesNumber", SqlDbType.Int);
                command.Parameters["@ToTotalRoommatesNumber"].Value = userSearch.ToTotalRoommatesNumber;
            }

            if (userSearch.Balcony > 0)
            {
                query += " and balconyId in (@BalconyId,0) ";
                command.Parameters.Add("@BalconyId", SqlDbType.Int);
                command.Parameters["@BalconyId"].Value = userSearch.Balcony;
            }
            if (userSearch.Furnitured > 0)
            {
                query += " and FurnituredId in (@FurnituredId,0) ";
                command.Parameters.Add("@FurnituredId", SqlDbType.Int);
                command.Parameters["@FurnituredId"].Value = userSearch.Furnitured;
            }
            if (userSearch.Renovated > 0)
            {
                query += " and RenovatedId in (@RenovatedId,0) ";
                command.Parameters.Add("@RenovatedId", SqlDbType.Int);
                command.Parameters["@RenovatedId"].Value = userSearch.Renovated;
            }
            if (userSearch.Sublet > 0)
            {
                query += " and SubletId in (@SubletId,0) ";
                command.Parameters.Add("@SubletId", SqlDbType.Int);
                command.Parameters["@SubletId"].Value = userSearch.Sublet;
            }
            if (userSearch.Pets > 0)
            {
                query += " and PetsId in (@PetsId,0) ";
                command.Parameters.Add("@PetsId", SqlDbType.Int);
                command.Parameters["@PetsId"].Value = userSearch.Pets;
            }
            if (userSearch.Parking > 0)
            {
                query += " and ParkingId in (@ParkingId,0) ";
                command.Parameters.Add("@ParkingId", SqlDbType.Int);
                command.Parameters["@ParkingId"].Value = userSearch.Parking;
            }
            if (userSearch.Smoke > 0)
            {
                query += " and SmokeId in (@SmokeId,0) ";
                command.Parameters.Add("@SmokeId", SqlDbType.Int);
                command.Parameters["@SmokeId"].Value = userSearch.Smoke;
            }
            if (userSearch.FromAgency > 0)
            {
                query += " and FromAgencyId in (@FromAgencyId,0) ";
                command.Parameters.Add("@FromAgencyId", SqlDbType.Int);
                command.Parameters["@FromAgencyId"].Value = userSearch.FromAgency;
            }
            if (userSearch.City > 0)
            {
                query += " and CityId = @CityId ";
                command.Parameters.Add("@CityId", SqlDbType.Int);
                command.Parameters["@CityId"].Value = userSearch.City;
            }

            command.CommandText = query;
            command.CommandType = CommandType.Text;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();
            
            int lastPostId = 0;
            House lastHouse = null;
            while (reader.Read())
            {
                if (lastPostId==(int)reader["postId"]){
                    lastHouse.Areas.Add((int)reader["realAreaId"]);
                }
                else{

                    if (lastHouse != null && lastPostId!=0)
                    {
                        result.Add(lastHouse);
                    }

                    House house = new House();

                    house.PostId = (int)reader["postId"];
                    house.CityId = (int)reader["cityId"];
                    house.RoomsNumber = Double.Parse(reader["roomsnumber"].ToString());
                    house.RoommatesNumber = (int)reader["totalRoommatesnumber"];
                    house.SideId = (int)reader["sideId"];
                    house.ParkingId = (int)reader["parkingId"];
                    house.RenovatedId = (int)reader["renovatedId"];
                    house.PurposeId = (int)reader["purposeId"];
                    house.Floor = (int)reader["floor"];
                    house.TypeId = (int)reader["typeId"];
                    house.id = (int)reader["id"];
                    house.Size = (int)reader["size"];
                    house.Price = (int)reader["price"];
                    house.HouseNumber = (int)reader["houseNumber"];
                    house.SmokeId = (int)reader["smokeId"];
                    house.PetsId = (int)reader["petsId"];
                    house.ElevatorId = (int)reader["elevatorId"];
                    house.BalconyId = (int)reader["balconyId"];
                    house.FurnituredId = (int)reader["furnituredId"];
                    house.SubletId = (int)reader["subletId"];
                    house.FromAgencyId = (int)reader["fromAgencyId"];
                    house.PhoneNumber = reader["phoneNumber"].ToString();
                    house.AddressesIds.Add(reader["addressConclusionId"].ToString());
                    house.DateCreated = DateTime.Parse(reader["dateCreated"].ToString());
                    house.UserSearchId = userSearch.id;
                    house.Message = reader["text"].ToString();

                    house.Areas.Add((int)reader["realAreaId"]);

                    lastHouse = house;
                    lastPostId = house.PostId;
                    
                }
            }

            if (lastHouse != null)
            {
                result.Add(lastHouse);
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal string GetSubAreaNameFromNeighborhoodId(int neighborhoodId, int cityId)
        {
            String subAreasTableName = GetSubAreasTableName(cityId);
            String neighborhoodsTableName = GetNeighborhoodsTableName(cityId);

            string result = "";
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select sub.name from " + subAreasTableName + " sub , " + neighborhoodsTableName + " n where sub.id=n.subAreaId and n.id = @NeighborhoodId";

            command.CommandType = CommandType.Text;

            command.Parameters.Add("@NeighborhoodId", SqlDbType.Int);
            command.Parameters["@NeighborhoodId"].Value = neighborhoodId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            if (reader.Read())
            {
                result = reader["name"].ToString();
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal List<HouseNames> GetApartmentsExact(UserSearch userSearch, DateTime startDate, DateTime endDate)
        {
            List<HouseNames> result = new List<HouseNames>();

            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            String query = @"Select * 
                            from v_filtered_houses_names h, posts p 
                            where h.postId = p.id and purposeId = @PurposeId 
                            and (h.dateCreated between @startDate and @endDate) ";

            command.Parameters.Add("@PurposeId", SqlDbType.Int);
            command.Parameters["@PurposeId"].Value = userSearch.Purpose;

            command.Parameters.Add("@startDate", SqlDbType.DateTime);
            command.Parameters["@startDate"].Value = startDate;

            command.Parameters.Add("@endDate", SqlDbType.DateTime);
            command.Parameters["@endDate"].Value = endDate;

            if (userSearch.FromSize > 0)
            {
                query += " and (size between @FromSize and @ToSize) ";
                command.Parameters.Add("@FromSize", SqlDbType.Int);
                command.Parameters["@FromSize"].Value = userSearch.FromSize;
                command.Parameters.Add("@ToSize", SqlDbType.Int);
                command.Parameters["@ToSize"].Value = userSearch.ToSize;
            }

            if (userSearch.FromPrice > 0)
            {
                query += " and (price between @FromPrice and @ToPrice) ";
                command.Parameters.Add("@FromPrice", SqlDbType.Int);
                command.Parameters["@FromPrice"].Value = userSearch.FromPrice;
                command.Parameters.Add("@ToPrice", SqlDbType.Int);
                command.Parameters["@ToPrice"].Value = userSearch.ToPrice;
            }

            if (userSearch.FromRoomsNumber > 0)
            {
                query += " and (roomsNumber between @FromRoomsNumber and @ToRoomsNumber) ";
                command.Parameters.Add("@FromRoomsNumber", SqlDbType.Decimal);
                command.Parameters["@FromRoomsNumber"].Value = userSearch.FromRoomsNumber;
                command.Parameters.Add("@ToRoomsNumber", SqlDbType.Decimal);
                command.Parameters["@ToRoomsNumber"].Value = userSearch.ToRoomsNumber;
            }

            if (userSearch.FromTotalRoommatesNumber > 0)
            {
                query += " and (totalRoommatesNumber between @FromTotalRoommatesNumber and @ToTotalRoommatesNumber) ";
                command.Parameters.Add("@FromTotalRoommatesNumber", SqlDbType.Int);
                command.Parameters["@FromTotalRoommatesNumber"].Value = userSearch.FromTotalRoommatesNumber;
                command.Parameters.Add("@ToTotalRoommatesNumber", SqlDbType.Int);
                command.Parameters["@ToTotalRoommatesNumber"].Value = userSearch.ToTotalRoommatesNumber;
            }

            if (userSearch.Balcony > 0)
            {
                query += " and balconyId = @BalconyId ";
                command.Parameters.Add("@BalconyId", SqlDbType.Int);
                command.Parameters["@BalconyId"].Value = userSearch.Balcony;
            }
            if (userSearch.Furnitured > 0)
            {
                query += " and FurnituredId = @FurnituredId ";
                command.Parameters.Add("@FurnituredId", SqlDbType.Int);
                command.Parameters["@FurnituredId"].Value = userSearch.Furnitured;
            }
            if (userSearch.Renovated > 0)
            {
                query += " and RenovatedId = @RenovatedId ";
                command.Parameters.Add("@RenovatedId", SqlDbType.Int);
                command.Parameters["@RenovatedId"].Value = userSearch.Renovated;
            }
            if (userSearch.Pets > 0)
            {
                query += " and PetsId in (@PetsId,0) ";
                command.Parameters.Add("@PetsId", SqlDbType.Int);
                command.Parameters["@PetsId"].Value = userSearch.Pets;
            }
            if (userSearch.Parking > 0)
            {
                query += " and ParkingId = @ParkingId ";
                command.Parameters.Add("@ParkingId", SqlDbType.Int);
                command.Parameters["@ParkingId"].Value = userSearch.Parking;
            }
            if (userSearch.Smoke > 0)
            {
                query += " and SmokeId = @SmokeId ";
                command.Parameters.Add("@SmokeId", SqlDbType.Int);
                command.Parameters["@SmokeId"].Value = userSearch.Smoke;
            }
            if (userSearch.FromAgency > (int)FromAgencyOptions.NONE)
            {
                if (userSearch.FromAgency == (int)FromAgencyOptions.NoFromAgency)
                {
                    query += " and FromAgencyId in (@FromAgencyId,0) ";
                }
                else
                {
                    query += " and FromAgencyId = @FromAgencyId ";
                }
                
                command.Parameters.Add("@FromAgencyId", SqlDbType.Int);
                command.Parameters["@FromAgencyId"].Value = userSearch.FromAgency;
            }
            if (userSearch.Sublet > (int)SubletOptions.NONE)
            {
                if (userSearch.Sublet == (int)SubletOptions.NoSublet)
                {
                    query += " and SubletId in (@SubletId,0) ";
                }
                else
                {
                    query += " and SubletId = @SubletId ";
                }

                command.Parameters.Add("@SubletId", SqlDbType.Int);
                command.Parameters["@SubletId"].Value = userSearch.FromAgency;
            }
            if (userSearch.City > 0)
            {
                query += " and CityId = @CityId ";
                command.Parameters.Add("@CityId", SqlDbType.Int);
                command.Parameters["@CityId"].Value = userSearch.City;
            }

            command.CommandText = query;
            command.CommandType = CommandType.Text;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                HouseNames house = new HouseNames();

                house.PostId = (int)reader["postId"];
                house.City = reader["city"].ToString();
                house.RoomsNumber = Double.Parse(reader["roomsnumber"].ToString());
                house.RoommatesNumber = (int)reader["totalRoommatesnumber"];
                house.Side = reader["side"].ToString();
                house.Parking = reader["parking"].ToString();
                house.Renovated = reader["renovated"].ToString();
                house.Purpose = reader["purpose"].ToString();
                house.Floor = (int)reader["floor"];
                house.Type = reader["type"].ToString();
                house.id = (int)reader["id"];
                house.Size = (int)reader["size"];
                house.Price = (int)reader["price"];
                house.Smoke = reader["smoke"].ToString();
                house.Pets = reader["pets"].ToString();
                house.Elevator = reader["elevator"].ToString();
                house.Balcony = reader["balcony"].ToString();
                house.Furnitured = reader["furnitured"].ToString();
                house.Sublet = reader["sublet"].ToString();
                house.FromAgency = reader["fromAgency"].ToString();
                house.AddressesIds.Add(reader["addressConclusionId"].ToString());
                house.DateCreated = DateTime.Parse(reader["dateCreated"].ToString());
                house.UserSearchId = userSearch.id;
                house.Message = reader["text"].ToString();

                result.Add(house);
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }


        internal House GetNewHouseById(string houseId)
        {
            House result = new House();
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"Select h.*, p.*
                                    from houses h, posts p
                                    where h.postId = p.id and h.id=@HouseId ";

            command.CommandType = CommandType.Text;
            command.Parameters.Add("@HouseId", SqlDbType.Int);
            command.Parameters["@HouseId"].Value = houseId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            while (reader.Read())
            {
                if (result == null)
                {
                    result = new House(reader["text"].ToString());
                }
                result.PostId = (int)reader["postId"];
                result.CityId = (int)reader["cityId"];
                result.RoomsNumber = Double.Parse(reader["roomsnumber"].ToString());
                result.RoommatesNumber = (int)reader["totalRoommatesnumber"];
                result.SideId = (int)reader["sideId"];
                result.ParkingId = (int)reader["parkingId"];
                result.RenovatedId = (int)reader["renovatedId"];
                result.Floor = (int)reader["floor"];
                result.TypeId = (int)reader["typeId"];
                result.id = (int)reader["id"];
                result.Size = (int)reader["size"];
                result.Price = (int)reader["price"];
                result.HouseNumber = (int)reader["houseNumber"];
                result.SmokeId = (int)reader["smokeId"];
                result.PetsId = (int)reader["petsId"];
                result.ElevatorId = (int)reader["elevatorId"];
                result.BalconyId = (int)reader["balconyId"];
                result.FurnituredId = (int)reader["furnituredId"];
                result.SubletId = (int)reader["subletId"];
                result.FromAgencyId = (int)reader["fromAgencyId"];
                result.PhoneNumber = reader["phoneNumber"].ToString();
                result.LocationsFound = reader["LocationsFound"].ToString();
                result.NeighborhoodsFound = reader["NeighborhoodsFound"].ToString();
                result.SubAreasFound = reader["SubAreasFound"].ToString();
                result.SubAreasFound = reader["SubAreasFound"].ToString();
                result.AreasFound = reader["AreasFound"].ToString();
                result.AddressesIds.Add(reader["addressConclusionId"].ToString());
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal String SaveUserSearch(UserSearch searchCriteria)
        {
            String result = String.Empty;
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                // create a SqlCommand object for this connection
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                Guid id = Guid.NewGuid();
                command.Parameters.Add("@Id", SqlDbType.NVarChar);
                command.Parameters["@Id"].Value = id.ToString();

                foreach (string currentId in searchCriteria.AddressConclusionIds)
                {

                    command.CommandText += @"insert into users_searches 
                                (id,userid,purposeid,subletId,fromprice,toprice,cityid,addressConclusionId,fromSize,toSize,fromroomsnumber,
                                toroomsnumber,fromTotalRoommatesNumber,parkingid,renovatedId,petsId,smokeId,furnituredId,balconyId,
                                fromAgencyId) 
                                values 
                                (@Id,@Userid,@Purposeid,@SubletId,@FromPrice,@ToPrice,@Cityid,'" + currentId + @"',@FromSize,
                                @ToSize,@FromRoomsNumber,@ToRoomsNumber,@FromTotalRoommatesNumber,@Parkingid,@RenovatedId,@PetsId,
                                @SmokeId,@FurnituredId,@BalconyId,@FromAgencyId);";
                }

                /*for (int i = 0; i < searchCriteria.SubAreas.Length; i++)
                {
                    command.Parameters.Add("@SubAreaId_" + i, SqlDbType.Int);
                    command.Parameters["@SubAreaId_" + i].Value = searchCriteria.SubAreas[i];

                    command.CommandText += @"insert into users_searches (id,userid,purposeid,subletId,fromprice,toprice,cityid,subareaId,fromSize,toSize,fromroomsnumber,toroomsnumber,fromTotalRoommatesNumber,parkingid,renovatedId,petsId,smokeId,furnituredId,balconyId,fromAgencyId) values (@Id,@Userid,@Purposeid,@SubletId,@FromPrice,@ToPrice,@Cityid,@SubAreaId_" + i + ",@FromSize,@ToSize,@FromRoomsNumber,@ToRoomsNumber,@FromTotalRoommatesNumber,@Parkingid,@RenovatedId,@PetsId,@SmokeId,@FurnituredId,@BalconyId,@FromAgencyId);";
                }

                for (int i = 0; i < searchCriteria.Locations.Length; i++)
                {
                    command.Parameters.Add("@LocationId_" + i, SqlDbType.Int);
                    command.Parameters["@LocationId_" + i].Value = searchCriteria.Locations[i];

                    command.CommandText += @"insert into users_searches (id,userid,purposeid,subletId,fromprice,toprice,cityid,locationId,fromSize,toSize,fromroomsnumber,toroomsnumber,fromTotalRoommatesNumber,parkingid,renovatedId,petsId,smokeId,furnituredId,balconyId,fromAgencyId) values (@Id,@Userid,@Purposeid,@SubletId,@FromPrice,@ToPrice,@Cityid,@LocationId_" + i + ",@FromSize,@ToSize,@FromRoomsNumber,@ToRoomsNumber,@FromTotalRoommatesNumber,@Parkingid,@RenovatedId,@PetsId,@SmokeId,@FurnituredId,@BalconyId,@FromAgencyId);";
                }*/

                command.Parameters.Add("@UserId", SqlDbType.Char);
                command.Parameters["@UserId"].Value = searchCriteria.UserId;

                command.Parameters.Add("@Purposeid", SqlDbType.Int);
                command.Parameters["@Purposeid"].Value = searchCriteria.Purpose;

                command.Parameters.Add("@SubletId", SqlDbType.Int);
                command.Parameters["@SubletId"].Value = searchCriteria.Sublet;

                command.Parameters.Add("@FromPrice", SqlDbType.Int);
                command.Parameters["@FromPrice"].Value = searchCriteria.FromPrice;

                command.Parameters.Add("@ToPrice", SqlDbType.Int);
                command.Parameters["@ToPrice"].Value = searchCriteria.ToPrice;

                command.Parameters.Add("@CityId", SqlDbType.Int);
                command.Parameters["@CityId"].Value = searchCriteria.City;

               /* command.Parameters.Add("@NeighborhoodId", SqlDbType.Int);
                command.Parameters["@NeighborhoodId"].Value = searchCriteria.NeighborhoodId;
            

                */
                command.Parameters.Add("@FromSize", SqlDbType.Int);
                command.Parameters["@FromSize"].Value = searchCriteria.FromSize;

                command.Parameters.Add("@ToSize", SqlDbType.Int);
                command.Parameters["@ToSize"].Value = searchCriteria.ToSize;

                command.Parameters.Add("@FromRoomsNumber", SqlDbType.Float);
                command.Parameters["@FromRoomsNumber"].Value = (float) searchCriteria.FromRoomsNumber;

                command.Parameters.Add("@ToRoomsNumber", SqlDbType.Float);
                command.Parameters["@ToRoomsNumber"].Value = (float) searchCriteria.ToRoomsNumber;

                command.Parameters.Add("@FromTotalRoommatesNumber", SqlDbType.Int);
                command.Parameters["@FromTotalRoommatesNumber"].Value = searchCriteria.FromTotalRoommatesNumber;

                command.Parameters.Add("@Parkingid", SqlDbType.Int);
                command.Parameters["@Parkingid"].Value = searchCriteria.Parking;
                
                command.Parameters.Add("@RenovatedId", SqlDbType.Int);
                command.Parameters["@RenovatedId"].Value = searchCriteria.Renovated;

                command.Parameters.Add("@PetsId", SqlDbType.Int);
                command.Parameters["@PetsId"].Value = searchCriteria.Pets;

                command.Parameters.Add("@SmokeId", SqlDbType.Int);
                command.Parameters["@SmokeId"].Value = searchCriteria.Smoke;

                command.Parameters.Add("@FurnituredId", SqlDbType.Int);
                command.Parameters["@FurnituredId"].Value = searchCriteria.Furnitured;

                command.Parameters.Add("@BalconyId", SqlDbType.Int);
                command.Parameters["@BalconyId"].Value = searchCriteria.Balcony;

                command.Parameters.Add("@FromAgencyId", SqlDbType.Int);
                command.Parameters["@FromAgencyId"].Value = searchCriteria.FromAgency;

                // execute the command that returns a SqlDataReader
                reader = command.ExecuteReader();

                result = id.ToString();
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            finally
            {
                // close the connection
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                connection.Close();

            }

            return result;
        }

        internal List<UserSearch> GetUserSearchesByIdsWithoutAddresses(List<string> searchIds)
        {
            List<UserSearch> usersSearches = new List<UserSearch>();
            connection.Open();

            string inArray = String.Empty;
            foreach (string currentSearchId in searchIds)
            {
                inArray += "'" + currentSearchId + "',";
            }
            inArray = inArray.Substring(0,inArray.Length-1);

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"select distinct cityId,FromRoomsNumber, ToRoomsNumber, FromTotalRoommatesnumber, 
                                    ToTotalRoommatesnumber, parkingId, renovatedId, purposeId, id,fromSize,toSize,  FromPrice,ToPrice,
                                    smokeId, petsId, balconyId, furnituredId, subletId, fromAgencyId 
                                    from users_searches where id in (" + inArray + ") order by id";
            command.CommandType = CommandType.Text;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            UserSearch userSearch = null;
            while (reader.Read())
            {
                if (userSearch == null)
                {
                    userSearch = new UserSearch();
                    userSearch.City = (int)reader["cityId"];
                    userSearch.FromRoomsNumber = Double.Parse(reader["FromRoomsNumber"].ToString());
                    userSearch.ToRoomsNumber = Double.Parse(reader["ToRoomsNumber"].ToString());
                    userSearch.FromTotalRoommatesNumber = (int)reader["FromTotalRoommatesnumber"];
                    userSearch.ToTotalRoommatesNumber = (int)reader["ToTotalRoommatesnumber"];
                    userSearch.Parking = (int)reader["parkingId"];
                    userSearch.Renovated = (int)reader["renovatedId"];
                    userSearch.Purpose = (int)reader["purposeId"];
                    userSearch.id = reader["id"].ToString();
                    //userSearch.AddressConclusionIds.Add(reader["addressConclusionId"].ToString());
                    userSearch.FromSize = (int)reader["fromSize"];
                    userSearch.ToSize = (int)reader["toSize"];
                    userSearch.FromPrice = (int)reader["FromPrice"];
                    userSearch.ToPrice = (int)reader["ToPrice"];
                    userSearch.Smoke = (int)reader["smokeId"];
                    userSearch.Pets = (int)reader["petsId"];
                    userSearch.Balcony = (int)reader["balconyId"];
                    userSearch.Furnitured = (int)reader["furnituredId"];
                    userSearch.Sublet = (int)reader["subletId"];
                    userSearch.FromAgency = (int)reader["fromAgencyId"];
                }
                else
                {
                    //userSearch.AddressConclusionIds.Add(reader["addressConclusionId"].ToString());
                }

                usersSearches.Add(userSearch);
            }

            // close the connection
            reader.Close();
            connection.Close();

            return usersSearches;
        }

        internal Dictionary<string, string> GetNeighborhoods(int cityId)
        {

            String neighborhoodsTableName = GetNeighborhoodsTableName(cityId);

            Dictionary<string, string> result = new Dictionary<string, string>();
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"select id,name from " + neighborhoodsTableName;

            command.CommandType = CommandType.Text;
            command.Parameters.Add("@CityId", SqlDbType.Int);
            command.Parameters["@CityId"].Value = cityId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            while (reader.Read())
            {

                string currentId = reader["id"].ToString();
                string currentName = reader["name"].ToString();

                result.Add(currentId ,currentName);
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal Dictionary<string, string> GetLocations(int cityId)
        {
            String locationsTableName = GetLocationsTableName(cityId);

            Dictionary<string, string> result = new Dictionary<string, string>();
            connection.Open();

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"select id,name from " + locationsTableName;

            command.CommandType = CommandType.Text;
            command.Parameters.Add("@CityId", SqlDbType.Int);
            command.Parameters["@CityId"].Value = cityId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            // display the results
            while (reader.Read())
            {

                string currentId = reader["id"].ToString();
                string currentName = reader["name"].ToString();

                result.Add(currentId, currentName);
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal void SaveHouse(House currentNewHouse)
        {
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                // create a SqlCommand object for this connection
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                command.CommandText = @"insert into houses (postId, cityId, roomsNumber, sideId, parkingId, renovatedId, purposeId, streetId, floor, totalRoommatesNumber, areaId, typeId, neighborhoodId, price, size,locationId, houseNumber, comment, petsId, smokeId, elevatorId, furnituredId, balconyId, subletId,streetComment,subareaid, fromAgencyId,areasFound,subAreasFound,NeighborhoodsFound,streetsFound,locationsFound) 
                                        values (@postId, @cityId, @roomsNumber, @sideId, @parkingId, @renovatedId, @purposeId, @streetId, 
                                                @floor, @totalRoommatesNumber, @areaId, @typeId, @neighborhoodId, @price, @size,@locationId, 
                                                @houseNumber, @comment, @petsId, @smokeId, @elevatorId, @furnituredId, @balconyId, 
                                                @subletId,@streetComment,@subareaid, @fromAgencyId,@areasFound,@subAreasFound,
                                                @neighborhoodsFound,@streetsFound,@locationsFound, @phoneNumber)";


                command.Parameters.Add("@subAreasFound", SqlDbType.NVarChar);
                command.Parameters["@subAreasFound"].Value = currentNewHouse.SubAreasFound;

                command.Parameters.Add("@neighborhoodsFound", SqlDbType.NVarChar);
                command.Parameters["@neighborhoodsFound"].Value = currentNewHouse.NeighborhoodsFound;

                command.Parameters.Add("@streetsFound", SqlDbType.NVarChar);
                command.Parameters["@streetsFound"].Value = currentNewHouse.StreetsFound;

                command.Parameters.Add("@locationsFound", SqlDbType.NVarChar);
                command.Parameters["@locationsFound"].Value = currentNewHouse.LocationsFound;

                command.Parameters.Add("@subletId", SqlDbType.Int);
                command.Parameters["@subletId"].Value = currentNewHouse.SubletId;

                command.Parameters.Add("@streetComment", SqlDbType.NVarChar);
                command.Parameters["@streetComment"].Value = currentNewHouse.StreetsComment;

                command.Parameters.Add("@subareaid", SqlDbType.Int);
                command.Parameters["@subareaid"].Value = currentNewHouse.SubletId;

                command.Parameters.Add("@fromAgencyId", SqlDbType.NVarChar);
                command.Parameters["@fromAgencyId"].Value = currentNewHouse.FromAgencyId;

                command.Parameters.Add("@areasFound", SqlDbType.NVarChar);
                command.Parameters["@areasFound"].Value = currentNewHouse.AreasFound;




                command.Parameters.Add("@petsId", SqlDbType.Int);
                command.Parameters["@petsId"].Value = currentNewHouse.PetsId;

                command.Parameters.Add("@smokeId", SqlDbType.Int);
                command.Parameters["@smokeId"].Value = currentNewHouse.SmokeId;

                command.Parameters.Add("@elevatorId", SqlDbType.Int);
                command.Parameters["@elevatorId"].Value = currentNewHouse.ElevatorId;

                command.Parameters.Add("@furnituredId", SqlDbType.Int);
                command.Parameters["@furnituredId"].Value = currentNewHouse.FurnituredId;

                command.Parameters.Add("@balconyId", SqlDbType.Int);
                command.Parameters["@balconyId"].Value = currentNewHouse.BalconyId;

                command.Parameters.Add("@phoneNumber", SqlDbType.NVarChar);
                command.Parameters["@phoneNumber"].Value = currentNewHouse.PhoneNumber;


                command.Parameters.Add("@price", SqlDbType.Int);
                command.Parameters["@price"].Value = currentNewHouse.Price;

                command.Parameters.Add("@size", SqlDbType.Int);
                command.Parameters["@size"].Value = currentNewHouse.Size;

                command.Parameters.Add("@comment", SqlDbType.NVarChar);
                command.Parameters["@comment"].Value = currentNewHouse.Comment;

                command.Parameters.Add("@houseNumber", SqlDbType.Int);
                command.Parameters["@houseNumber"].Value = currentNewHouse.HouseNumber;

                command.Parameters.Add("@typeId", SqlDbType.Int);
                command.Parameters["@typeId"].Value = currentNewHouse.TypeId;



                command.Parameters.Add("@floor", SqlDbType.Int);
                command.Parameters["@floor"].Value = currentNewHouse.Floor;

                command.Parameters.Add("@totalRoommatesNumber", SqlDbType.Int);
                command.Parameters["@totalRoommatesNumber"].Value = currentNewHouse.RoommatesNumber;



                command.Parameters.Add("@postId", SqlDbType.Int);
                command.Parameters["@postId"].Value = currentNewHouse.PostId;

                command.Parameters.Add("@cityId", SqlDbType.Int);
                command.Parameters["@cityId"].Value = currentNewHouse.CityId;

                command.Parameters.Add("@roomsNumber", SqlDbType.Decimal);
                command.Parameters["@roomsNumber"].Value = (Decimal) currentNewHouse.RoomsNumber;

                command.Parameters.Add("@sideId", SqlDbType.Int);
                command.Parameters["@sideId"].Value = currentNewHouse.SideId;

                command.Parameters.Add("@parkingId", SqlDbType.Int);
                command.Parameters["@parkingId"].Value = currentNewHouse.ParkingId;

                command.Parameters.Add("@renovatedId", SqlDbType.Int);
                command.Parameters["@renovatedId"].Value = currentNewHouse.RenovatedId;

                command.Parameters.Add("@purposeId", SqlDbType.Int);
                command.Parameters["@purposeId"].Value = currentNewHouse.PurposeId;


                // execute the command that returns a SqlDataReader
                reader = command.ExecuteReader();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                // close the connection
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                connection.Close();

            }


            
        }

        internal void DeleteHouse(int id)
        {
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                // create a SqlCommand object for this connection
                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.Text;

                command.CommandText = @"delete from houses where id = @id";


                command.Parameters.Add("@id", SqlDbType.NVarChar);
                command.Parameters["@id"].Value = id;

                reader = command.ExecuteReader();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                // close the connection
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                connection.Close();

            }


        }

        internal List<AddressConclusion> GetHouseAddressesNames(ref List<House> houses)
        {
            List<AddressConclusion> result = new List<AddressConclusion>();
            SqlDataReader reader = null;
            try
            {
                connection.Open();

                foreach (House currentHouse in houses)
                {
                    foreach (String currentAddressConclusionId in currentHouse.AddressesIds)
                    {
                        AddressConclusion addressConclusion = null;
                        // create a SqlCommand object for this connection
                        SqlCommand command = connection.CreateCommand();
                        command.CommandText = @"select * from houseAddressConclusions where id = @Id";

                        command.CommandType = CommandType.Text;
                        command.Parameters.Add("@Id", SqlDbType.Int);
                        command.Parameters["@Id"].Value = currentAddressConclusionId;

                        // execute the command that returns a SqlDataReader
                        reader = command.ExecuteReader();

                        // display the results
                        while (reader.Read())
                        {
                            if (addressConclusion == null)
                            {
                                addressConclusion = new AddressConclusion();
                            }

                            int objectType = (int)reader["objectType"];
                            if (objectType == 1)
                            {
                                int subAreaId = (int)reader["subAreaId"];
                                addressConclusion.SubAreasIds.Add(subAreaId);
                            }
                            else if (objectType == 2)
                            {
                                int locationId = (int)reader["locationId"];
                                addressConclusion.LocationsIds.Add(locationId);
                            }
                        }

                        if (addressConclusion != null)
                        {
                            result.Add(addressConclusion);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                // close the connection
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                connection.Close();

            }

            return result;
        }

        internal List<AddressConclusion> GetHouseAddressesIds(List<String> addressesIds)
        {
            List<AddressConclusion> result = new List<AddressConclusion>();
            SqlDataReader reader = null;

            foreach (String currentAddressConclusionId in addressesIds)
            {
                try
                {

                    connection.Open();
                    SqlCommand command = connection.CreateCommand();

                    AddressConclusion addressConclusion = null;
                    // create a SqlCommand object for this connection
                    
                    command.CommandText = @"select * from houseAddressConclusions where id = @Id";

                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@Id", SqlDbType.NVarChar);
                    command.Parameters["@Id"].Value = currentAddressConclusionId;

                    // execute the command that returns a SqlDataReader
                    reader = command.ExecuteReader();

                    // display the results
                    while (reader.Read())
                    {
                        if (addressConclusion == null){
                            addressConclusion = new AddressConclusion();
                        }

                        int objectType = (int)reader["objectType"];
                        int objectId = (int)reader["objectId"];
                        if (objectType == 1)
                        {
                            addressConclusion.SubAreasIds.Add(objectId);
                        }
                        else if (objectType == 2)
                        {
                            addressConclusion.LocationsIds.Add(objectId);
                        }
                    }

                    if (addressConclusion != null)
                    {
                        result.Add(addressConclusion);
                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    // close the connection
                    if (reader != null && !reader.IsClosed)
                    {
                        reader.Close();
                    }
                    connection.Close();

                }
            }

            return result;
        }

        public List<string> GetAddressConclusionsByObjectIds(int[] objectIds, int objectType)
        {
            List<string> result = new List<string>();
            SqlDataReader reader = null;

            try
            {

                connection.Open();
                SqlCommand command = connection.CreateCommand();

                AddressConclusion addressConclusion = null;
                // create a SqlCommand object for this connection

                string inArray = String.Empty;
                for (int i = 0; i < objectIds.Length; i++)
                {
                    inArray += objectIds[i] + ",";
                }
                if (!String.IsNullOrEmpty(inArray))
                {
                    inArray = inArray.Substring(0, inArray.Length-1);
                }


                command.CommandText = @"select * 
                                        from houseaddressconclusions 
                                        where id in (
	                                        select id
	                                        from houseAddressConclusions 
	                                        where id in (
                                                select distinct id
	                                            from houseAddressConclusions
	                                            where objectType=@ObjectType
	                                            and objectId in (" + inArray + @"))
	                                        group by id
	                                        having count(distinct objectId) <=" + objectIds.Length + @"
                                            except
	                                        select id from houseaddressconclusions where id in (
		                                        select id
		                                        from houseAddressConclusions 
		                                        where id in (select distinct id
		                                        from houseAddressConclusions
		                                        where objectType=@ObjectType
		                                        and objectId in (" + inArray + @"))
		                                    group by id
		                                    having count(distinct objectId) <=" + objectIds.Length + @")
	                                        and objectid not in (" + inArray + @"))";
              
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@ObjectType", SqlDbType.NVarChar);
                command.Parameters["@ObjectType"].Value = objectType;

                // execute the command that returns a SqlDataReader
                reader = command.ExecuteReader();

                // display the results
                while (reader.Read())
                {
                    result.Add(reader["id"].ToString());
                }
            }
            catch (Exception ex)
            {
                result.Add(ex.Message);
            }
            finally
            {
                // close the connection
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                connection.Close();

            }

            return result;
        }

        internal List<BusinessReportResult> GetAllFromBusinessHouses(DateTime startDate, DateTime endDate)
        {
            List<BusinessReportResult> result = new List<BusinessReportResult>();
            SqlDataReader reader = null;

            try
            {

                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"select * from v_business_Report where dateCreated between @StartDate and @EndDate";

                command.CommandType = CommandType.Text;

                command.Parameters.Add("@StartDate", SqlDbType.DateTime);
                command.Parameters["@StartDate"].Value = startDate;

                command.Parameters.Add("@EndDate", SqlDbType.DateTime);
                command.Parameters["@EndDate"].Value = endDate;

                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    BusinessReportResult record = new BusinessReportResult();
                    record.DateCreated = DateTime.Parse(reader["dateCreated"].ToString());
                    record.PostText = reader["text"].ToString();
                    record.Sender = reader["sender"].ToString();
                    record.City = reader["cityName"].ToString();

                    result.Add(record);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                // close the connection
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                }
                connection.Close();

            }

            return result;
        }

        internal List<string> GetAddressConclusionIdsFromUserSearches(List<string> searches)
        {
            List<string> result = new List<string>();
            connection.Open();

            string inArray = String.Empty;
            foreach (string currentSearchId in searches)
            {
                inArray += "'" + currentSearchId + "',";
            }
            inArray = inArray.Substring(0, inArray.Length - 1);

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"select distinct addressConclusionId from users_searches where id in (" + inArray + ") ";
            command.CommandType = CommandType.Text;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                result.Add(reader["addressConclusionId"].ToString());

            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal Dictionary<string, string> GetAreasByCityId(int cityId)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            connection.Open();

            String areasTable = GetAreasTableName(cityId);

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"select * from " + areasTable;
            command.CommandType = CommandType.Text;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                result.Add(reader["id"].ToString(), reader["name"].ToString());
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal Dictionary<string, string> GetSubAreasByCityId(int cityId)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            connection.Open();

            String subAreasTable = GetSubAreasTableName(cityId);

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"select * from " + subAreasTable;
            command.CommandType = CommandType.Text;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                result.Add(reader["id"].ToString(), reader["name"].ToString());
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal Dictionary<string, string> GetLocationsByCityId(int cityId)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            connection.Open();

            String locationsTable = GetLocationsTableName(cityId);

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"select * from " + locationsTable;
            command.CommandType = CommandType.Text;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                result.Add(reader["id"].ToString(), reader["name"].ToString());
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }


        internal List<string> GetLocationsByAreaId(int currentAreaId, int cityId)
        {
            List<string> result = new List<string>();
            connection.Open();

            String locationsTable = GetLocationsTableName(cityId);
            String locationsToNeighborhoodsTable = GetLocationsToNeighborhoodsTableName(cityId);
            String neighborhoodsTable = GetNeighborhoodsTableName(cityId);
            String subAreasTable = GetSubAreasTableName(cityId);

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"select distinct l.id,l.name from " + locationsTable + @" l, " + locationsToNeighborhoodsTable + " ln, " + neighborhoodsTable + " n, " + subAreasTable + @" sa
                                    where l.id = ln.locationId and ln.neighborhoodId = n.id and n.subAreaId = sa.id and sa.areaId = @AreaId"; 
            command.CommandType = CommandType.Text;

            command.Parameters.Add("@AreaId", SqlDbType.Int);
            command.Parameters["@AreaId"].Value = currentAreaId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                result.Add(reader["id"].ToString());
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }

        internal List<string> GetSubAreasByAreaId(int currentAreaId, int cityId)
        {
            List<string> result = new List<string>();
            connection.Open();

            String locationsTable = GetLocationsTableName(cityId);
            String locationsToNeighborhoodsTable = GetLocationsToNeighborhoodsTableName(cityId);
            String subAreasTable = GetSubAreasTableName(cityId);

            // create a SqlCommand object for this connection
            SqlCommand command = connection.CreateCommand();
            command.CommandText = @"select * from " + subAreasTable + @" sa
                                    where sa.areaId = @AreaId";
            command.CommandType = CommandType.Text;

            command.Parameters.Add("@AreaId", SqlDbType.Int);
            command.Parameters["@AreaId"].Value = currentAreaId;

            // execute the command that returns a SqlDataReader
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                result.Add(reader["id"].ToString());
            }

            // close the connection
            reader.Close();
            connection.Close();

            return result;
        }
    }
}