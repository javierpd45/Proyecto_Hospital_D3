using System.Linq.Expressions;
using Hospital.Application.Persistence;
using Hospital.Domain;
using MediatR;

namespace Hospital.Application.Features.Perfiles.Queries.GetPerfilList;

public class GetPerfilListQueryHandler : IRequestHandler<GetPerfilListQuery, List<Perfil>> //Para transformarlo en un QuetyHandler
                                                                                           //implementa la interfaz IRequestHandler
{
    private readonly IUnitOfWork _unitOfWork;

    public GetPerfilListQueryHandler(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }

    public async Task<List<Perfil>> Handle(GetPerfilListQuery request, CancellationToken cancellationToken)
    {
        var includes = new List<Expression<Func<Perfil, object>>>();
        includes.Add(p => p.Usuarios!);

        var perfiles = await this._unitOfWork.Repository<Perfil>().GetAsync(
            null,
            x => x.OrderBy(y => y.NombrePerfil),
            includes,
            true
        );

        return new List<Perfil>(perfiles);
    }
}