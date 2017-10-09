export class EncodeUriHelper {
  encodeBase64(str: string): string {
    return btoa(
      encodeURIComponent(str)
        .replace(/%([0-9A-F]{2})/g, (match, p1) => String.fromCharCode(parseInt('0x' + p1, 16)))
    ).replace(/\+/ig, '-').replace(/=/ig, '_');
  }

  decodeBase64(str: string): string {
    return decodeURIComponent(
      atob(str.replace(/-/ig, '+').replace(/_/ig, '=')).split('')
        .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    );
  }
}
