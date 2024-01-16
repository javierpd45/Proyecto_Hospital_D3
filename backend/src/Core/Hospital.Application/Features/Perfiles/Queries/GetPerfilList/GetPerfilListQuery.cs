using Hospital.Domain;
using MediatR;

namespace Hospital.Application.Features.Perfiles.Queries.GetPerfilList;

public class GetPerfilListQuery : IRequest<List<Perfil>> //Se transforma en un objeto de tipo Query 
                                                        //por heredar de IRequest
                                                        //y devuelve una lista de Perfiles
{
}