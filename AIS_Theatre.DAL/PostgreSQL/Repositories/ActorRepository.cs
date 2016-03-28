using AIS_Theatre.Data;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Theatre.DAL
{
    public class ActorRepository : BaseRepository<Guid, Actor>, IActorRepository
    {
        public ActorRepository(NpgsqlConnection connection, NpgsqlTransaction transaction) : base(connection, transaction) { }

        public override Guid Insert(Actor entity)
        {
            return (Guid)
                base.ExecuteScalar<Guid>(
                        "insert into actor (id_actor, full_name_actor, birth_date_actor, salary_actor, experience_actor, height_actor, voice_actor, gender_actor, skin_color_actor, hair_color_actor) values (@Id, @Name, @BirthDate, @Salary, @Experience, @Height, @Voice, @Gender, @SkinColor, @HairColor) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Id", entity.Id.ToString()},
                            {"Name", entity.FullName},
                            {"BirthDate", entity.BirthDate},
                            {"Salary", entity.Salary},
                            {"Experience", entity.Experience},
                            {"Height", entity.ActorsFeatures.Height},
                            {"Voice", entity.ActorsFeatures.Voice },
                            {"Gender", entity.ActorsFeatures.Gender },
                            {"SkinColor", entity.ActorsFeatures.SkinColor },
                            {"HairColor", entity.ActorsFeatures.HairColor },
                        }
                    );

        }

        public override bool Update(Actor entity)
        {
            var res = base.ExecuteNonQuery(
                    "update actor set full_name_actor = @Name, birth_date_actor = @BirthDate, salary_actor = @Salary, experience_actor = @Experience, height_actor = @Height, voice_actor = @Voice, gender_actor = @Gender, skin_color_actor = @SkinColor, hair_color_actor = @HairColor where id_actor = @Id ",
                    new SqlParameters
                    {
                            {"Id", entity.Id.ToString()},
                            {"Name", entity.FullName},
                            {"BirthDate", entity.BirthDate},
                            {"Salary", entity.Salary},
                            {"Experience", entity.Experience},
                            {"Height", entity.ActorsFeatures.Height},
                            {"Voice", entity.ActorsFeatures.Voice },
                            {"Gender", entity.ActorsFeatures.Gender },
                            {"SkinColor", entity.ActorsFeatures.SkinColor },
                            {"HairColor", entity.ActorsFeatures.HairColor },
                    }
                );

            return res > 0;
        }

        public Actor GetById(Guid id)
        {
            return base.ExecuteSingleRowSelect(
                    "select * from actor where id_actor = @Id",
                    new SqlParameters()
                    {
                        {"Id", id.ToString()}
                    }
                );
        }

        public bool Delete(Guid id)
        {
            var res = base.ExecuteNonQuery(
                "delete from actor where id_actor = @Id",
                new SqlParameters()
                {
                    { "Id", id.ToString() }
                });

            if (res > 1)
                throw new InvalidOperationException("Multiple rows deleted by single delete query");

            return res == 1;
        }

        public List<Actor> GetAll()
        {
            return base.ExecuteSelect("Select * from actor");
        }

        public List<Actor> GetByFeatures(ActorsFeatures features)
        {
            return base.ExecuteSelect(
                        "Select * from actor where height_actor = @Height AND voice_actor = @Voice AND gender_actor = @Gender AND skin_color_actor = @SkinColor AND hair_color_actor = @HairColor",
                        new SqlParameters
                        {
                            {"Height", features.Height},
                            {"Voice", features.Voice },
                            {"Gender", features.Gender },
                            {"SkinColor", features.SkinColor },
                            {"HairColor", features.HairColor },
                        });
        }

        public List<Actor> GetActorsOfPerformance(Performance performance)
        {
            return base.ExecuteSelect(
                        "select * from actor_performance left join actor on actor.id_actor = actor_performance.id_actor_actor_performance where actor_performance.id_performance_actor_performance = @Performance",
                        new SqlParameters
                        {
                            {"Performance", performance.Id.ToString()}
                        });
        }

        public List<Actor> GetByBirthDate(string birthDate)
        {
            return base.ExecuteSelect(
                        "Select * from actor where birth_date_actor = @BirthDate",
                        new SqlParameters
                        {
                            {"BirthDate", birthDate},
                        });
        }

        public List<Actor> GetByExperience(string experince)
        {
            return base.ExecuteSelect(
                        "Select * from actor where experience_actor = @Experince",
                        new SqlParameters
                        {
                            {"Experince", experince},
                        });
        }

        public List<Actor> GetBySalary(int salary)
        {
            return base.ExecuteSelect(
                        "Select * from actor where salary_actor = @Salary",
                        new SqlParameters
                        {
                            {"Salary", salary},
                        });
        }

        protected override Actor DefaultRowMapping(NpgsqlDataReader reader)
        {
            Actor stunt = null;
            if (Guid.Parse((string)reader["id_stunt_actor"]) != Guid.Empty)
            {
                ActorRepository actorRepository = new ActorRepository(_connection, _transaction);
                stunt = actorRepository.GetById(Guid.Parse((string)reader["id_stunt_actor"]));
            }
            ActorsFeatures features = new ActorsFeatures((string)reader["height_actor"], (string)reader["voice_actor"], (string)reader["gender_actor"], (string)reader["skin_color_actor"], (string)reader["hair_color_actor"]);
            return new Actor(Guid.Parse((string)reader["id_actor"]), (string)reader["full_name_actor"], (string)reader["birth_date_actor"], (int)reader["salary_actor"], (string)reader["experience_actor"], features, stunt);
        }
    }
}
