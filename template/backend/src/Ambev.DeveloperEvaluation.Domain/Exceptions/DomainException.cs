// Namespace adicionado para permitir referência qualificada da classe a partir de outros projetos
// A classe existia sem namespace, tornando-a inacessível via caminho completo nas entidades Sale e SaleItem.
namespace Ambev.DeveloperEvaluation.Domain.Exceptions;
public class DomainException:Exception
{
    public DomainException(string message):base(message)
    {
    }

    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
