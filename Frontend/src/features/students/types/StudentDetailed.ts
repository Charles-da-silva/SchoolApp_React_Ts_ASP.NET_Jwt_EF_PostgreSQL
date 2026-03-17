export interface StudentAnamnesis {
  id: string
  content: string
  createdAt: string
  updatedAt?: string
}

export interface StudentDetailed {
  id: string
  fullName: string
  dateOfBirth: string
  documentType: string
  documentNumber: string
  isActive: boolean
  createdAt: string
  deactivatedAt?: string
  anamnesis?: StudentAnamnesis
}