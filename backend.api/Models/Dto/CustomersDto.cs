namespace backend.api.Models.dto;

public record CustomersDto(int Id, string Firstname, string Middlename, string Lastname, int Age, string Sex, bool IsEmployee);
