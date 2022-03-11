using System;

namespace AtividadeScrumLetsCode.Entities
{
    public class EntidadeBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
