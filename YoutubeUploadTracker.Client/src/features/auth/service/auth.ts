import { backendUrl } from "../../common/utils/urls";
import { UserInfo } from "../types";

export const fetchUserDetails = async (
  cookies: string
): Promise<UserInfo | undefined> => {
  const response = await fetch(`${backendUrl}/user`, {
    headers: {
      Cookie: cookies,
    },
  });

  if (response.status !== 200) {
    return undefined;
  }

  return await response.json();
};

export const login = async () => {
  window.location.href = `${backendUrl}/login`;
};

export const logout = async () => {
  await fetch(`${backendUrl}/logout`, {
    method: "POST",
  });
  window.location.href = "/";
};
