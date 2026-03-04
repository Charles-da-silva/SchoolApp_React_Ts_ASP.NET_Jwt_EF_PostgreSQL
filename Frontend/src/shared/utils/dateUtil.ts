/* Essa função formata uma data ISO (YYYY-MM-DD) para o padrão 
brasileiro (DD/MM/YYYY).

NÃO usamos new Date() para evitar problemas de fuso horário.
Trabalhamos diretamente com string, porque Seu backend envia algo assim:

2025-03-03  Esse formato é ISO (YYYY-MM-DD). O navegador, quando usa 
new Date(), tenta interpretar isso como: 

    2025-03-03T00:00:00.000Z

O Z significa UTC.
Se você estiver no Brasil (UTC-3), ele converte para:

2025-03-02 21:00:00

E aí aparece 1 dia a menos. Isso é o famoso bug de timezone.
*/
export function formatDateToPtBr(date: string): string {
  const [year, month, day] = date.split("-");
  return `${day}/${month}/${year}`;
}