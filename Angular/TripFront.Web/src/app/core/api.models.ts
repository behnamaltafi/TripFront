export interface AuthResponse {
  success: boolean;
  userId: string;
  email: string | null;
  errors?: string[];
}

export interface LoginRequest {
  userName: string;
  password: string;
}

export interface RegisterRequest {
  email: string;
  username: string;
  password: string;
  firstName: string;
  lastName: string;
  accountNumber?: string;
}

export interface Family {
  id: number;
  name: string;
  accountNumber: string | null;
}

export interface Trip {
  id: number;
  title: string;
  description: string | null;
  startDate: string;
  endDate: string | null;
  budget: number;
  ownerFamilyName: string | null;
  families: Family[];
}

export interface CreateTripRequest {
  title: string;
  description: string;
  memberCount: number;
  startDate: string;
  endDate: string | null;
}

export interface PagedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}
